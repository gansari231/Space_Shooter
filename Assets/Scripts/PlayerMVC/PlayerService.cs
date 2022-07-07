using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : SingletonGeneric<PlayerService>
{
    [SerializeField]
    PlayerView _playerView;
    public PlayerController _playerController;

    void Start()
    {
        CreateShip();
    }
   
    private void CreateShip()
    {
        PlayerModel _playerModel = new PlayerModel();
        _playerController = new PlayerController(_playerView, _playerModel);
    }
}
