using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public float _speed;
    public float _speedBoostMultiplier;
    public float _fireRate;
    public int _bulletsFired;

    public PlayerModel()
    {
        _speed = 5.0f;
        _speedBoostMultiplier = 2.0f;
        _fireRate = 0.2f;
    }
}
