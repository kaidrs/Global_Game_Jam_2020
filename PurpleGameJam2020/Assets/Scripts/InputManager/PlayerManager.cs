﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager _instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private List<Player> _players;
    public List<Player> Players { get => _players; set => _players = value; }

    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        switch (Gamepad.all.Count)
        {
            case 0:
                _playerInputManager.JoinPlayer();
                _playerInputManager.JoinPlayer();
                break;
            case 1:
                _playerInputManager.JoinPlayer();
                _playerInputManager.JoinPlayer(-1, -1, null, Gamepad.all[0]);
                break;
            case 2:
                _playerInputManager.JoinPlayer(-1, -1, null, Gamepad.all[0]);
                _playerInputManager.JoinPlayer(-1, -1, null, Gamepad.all[1]);
                break;
            default:
                break;
        }
    }
    public void AddPlayer(Player player)
    {
        _players.Add(player);
        var indexOfPlayer = _players.IndexOf(player);
        Debug.Log(indexOfPlayer);
        player.ChangeColor(player.Colors[indexOfPlayer]);
        if (_players.Count == 1)
        {
            player.tag = "Player";
            player.gameObject.layer = LayerMask.NameToLayer("Player1");
            UIManager.Instance.P1 = player;
            
            
        }
        else if (_players.Count == 2)
        {
            player.tag = "Player2";
            player.gameObject.layer = LayerMask.NameToLayer("Player2");
            UIManager.Instance.P2 = player;

        }
    }
}