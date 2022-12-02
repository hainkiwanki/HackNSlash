using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{ 
    public static void Welcome(Packet _packet)
    {
        string msg = _packet.ReadString();
        int id = _packet.ReadInt();

        Debug.Log($"Message from server: {msg}");
        Client.Inst.myId = id;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.Inst.udp.Connect(((IPEndPoint)Client.Inst.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.Inst.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerMovement(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        if (GameManager.players.ContainsKey(_id))
        {
            if (GameManager.players[_id].transform.position != _position && !GameManager.players[_id].snapShotPositions.Contains(_position))
            {
                GameManager.players[_id].snapShotPositions.Add(_position);
            }
            // GameManager.players[_id].transform.position = _position;
            GameManager.players[_id].transform.rotation = _rotation;
        }
    }

    public static void PlayerAnimation(Packet _packet)
    {
        int id = _packet.ReadInt();
        float vel = _packet.ReadFloat();

        GameManager.Inst.SetAnimParams(id, vel);
    }
}
