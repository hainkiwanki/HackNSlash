using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : Singleton<Client>
{
    public static int dataBufferSize = 4096;

    public string ip = "127.0.0.1";
    public int port = 26950;
    public int myId = 0;
    public TCP tcp;
    public UDP udp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    protected override void _OnAwake()
    {
        base._OnAwake();
    }

    private void Start()
    {
        tcp = new TCP();
        udp = new UDP();
    }

    private void OnApplicationQuit()
    {
        Disconnect(); // Disconnect when the game is closed
    }

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        InitializeClientData();

        isConnected = true;
        tcp.Connect(); // Connect tcp, udp gets connected once tcp is done
    }

    public class TCP
    {
        public TcpClient socket;

        private NetworkStream stream;
        private Packet receivedData;
        private byte[] receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };

            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(Inst.ip, Inst.port, ConnectCallback, socket);
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
                return;

            stream = socket.GetStream();

            receivedData = new Packet();

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        /// <summary>Sends data to the client via TCP.</summary>
        /// <param name="_packet">The packet to send.</param>
        public void SendData(Packet _packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null); // Send data to server
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via TCP: {_ex}");
            }
        }


        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                int byteLength = stream.EndRead(_result);
                if (byteLength <= 0)
                {
                    Inst.Disconnect();
                    return;
                }
                byte[] data = new byte[byteLength];
                Array.Copy(receiveBuffer, data, byteLength);

                receivedData.Reset(HandleData(data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error receiving TCP data: {_ex}.");
                Disconnect();
            }
        }

        /// <summary>Prepares received data to be used by the appropriate packet handler methods.</summary>
        /// <param name="_data">The recieved data.</param>
        private bool HandleData(byte[] _data)
        {
            int packetLength = 0;

            receivedData.SetBytes(_data);

            if (receivedData.UnreadLength() >= 4)
            {
                // If client's received data contains a packet
                packetLength = receivedData.ReadInt();
                if (packetLength <= 0)
                {
                    // If packet contains no data
                    return true; // Reset receivedData instance to allow it to be reused
                }
            }

            while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
            {
                // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
                byte[] _packetBytes = receivedData.ReadBytes(packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(_packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        packetHandlers[packetId](packet); // Call appropriate method to handle the packet
                    }
                });

                packetLength = 0; // Reset packet length
                if (receivedData.UnreadLength() >= 4)
                {
                    // If client's received data contains another packet
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        // If packet contains no data
                        return true; // Reset receivedData instance to allow it to be reused
                    }
                }
            }

            if (packetLength <= 1)
            {
                return true; // Reset receivedData instance to allow it to be reused
            }

            return false;
        }

        /// <summary>Disconnects from the server and cleans up the TCP connection.</summary>
        private void Disconnect()
        {
            Inst.Disconnect();

            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;
        }
    }

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(Inst.ip), Inst.port);
        }

        /// <summary>Attempts to connect to the server via UDP.</summary>
        /// <param name="_localPort">The port number to bind the UDP socket to.</param>
        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet packet = new Packet())
            {
                SendData(packet);
            }
        }

        /// <summary>Sends data to the client via UDP.</summary>
        /// <param name="_packet">The packet to send.</param>
        public void SendData(Packet _packet)
        {
            try
            {
                _packet.InsertInt(Inst.myId); // Insert the client's ID at the start of the packet
                if (socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via UDP: {_ex}");
            }
        }

        /// <summary>Receives incoming UDP data.</summary>
        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if (data.Length < 4)
                {
                    Inst.Disconnect();
                    return;
                }

                HandleData(data);
            }
            catch
            {
                Disconnect();
            }
        }

        /// <summary>Prepares received data to be used by the appropriate packet handler methods.</summary>
        /// <param name="_data">The recieved data.</param>
        private void HandleData(byte[] _data)
        {
            using (Packet packet = new Packet(_data))
            {
                int packetLength = packet.ReadInt();
                _data = packet.ReadBytes(packetLength);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    int packetId = _packet.ReadInt();
                    packetHandlers[packetId](_packet); // Call appropriate method to handle the packet
                }
            });
        }

        /// <summary>Disconnects from the server and cleans up the UDP connection.</summary>
        private void Disconnect()
        {
            Inst.Disconnect();

            endPoint = null;
            socket = null;
        }
    }

    /// <summary>Initializes all necessary client data.</summary>
    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, ClientHandle.Welcome },
            { (int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer },
            { (int)ServerPackets.playerMovement, ClientHandle.PlayerMovement },
            { (int)ServerPackets.playerAnimation, ClientHandle.PlayerAnimation },
        };
        Debug.Log("Initialized packets.");
    }

    /// <summary>Disconnects from the server and stops all network traffic.</summary>
    private void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;
            tcp.socket.Close();
            udp.socket.Close();

            Debug.Log("Disconnected from server.");
        }
    }
}
