using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    protected override void _OnAwake()
    {
        base._OnAwake();
    }

    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.Inst.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void SetAnimParams(int _id, float _vel)
    {
        if(players.ContainsKey(_id))
        {
            players[_id].GetComponent<PlayerManager>().velocity = _vel;
        }
    }
}
