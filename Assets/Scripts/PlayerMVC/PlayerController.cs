using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public PlayerView _playerView;
    public PlayerModel _playerModel;

    public GameObject bullet;

    public float _canFire = -1.0f;

    private int _lives = 3;
    private int _score = 0;

    public bool _isTripleShotActive = false;
    private bool _isShieldsActive = false;
    public bool _isPlayerOne = false;

    public Vector3 offset;

    public PlayerController(PlayerView _playerView, PlayerModel _playerModel)
    {
        this._playerModel = _playerModel;
        this._playerView = GameObject.Instantiate(_playerView);
        this._playerView.SetTankControllerReference(this);

        SubscribeEvents();
    }

    void SubscribeEvents()
    {
        EventHandler.Instance.OnBulletFired += UpdateBulletsFiredCount;
    }

    void UnSubscribeEvents()
    {
        EventHandler.Instance.OnBulletFired -= UpdateBulletsFiredCount;
    }

    private void UpdateBulletsFiredCount()
    {
        _playerModel._bulletsFired++;
        AchievementSystem.Instance.BulletsFiredCountCheck(_playerModel._bulletsFired);
    }

    public void CheckFireButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            PlayerOneFireLaser();          
        }
    }

    public void SetPlayerBounds()
    {
        _playerView.transform.position = new Vector3(_playerView.transform.position.x, Mathf.Clamp(_playerView.transform.position.y, -3.8f, 0), 0);

        if (_playerView.transform.position.x >= 11.5f)
        {
            _playerView.transform.position = new Vector3(-11.5f, _playerView.transform.position.y, 0);
        }
        else if (_playerView.transform.position.x <= -11.5f)
        {
            _playerView.transform.position = new Vector3(11.5f, _playerView.transform.position.y, 0);
        }
    }

    public void PlayerOneFireLaser()
    {
        _canFire = Time.time + _playerModel._fireRate;
        AudioManager.Instance.PlayLaserAudio();
        if (_isTripleShotActive == true)
        {
            _playerView.SpawnTripleBullet();
        }
        else
        {
            offset = _playerView.transform.position + new Vector3(0, 1.2f, 0);
            bullet = ObjectPool.Instance.GetPooledObject();
            if (bullet != null)
            {
                _playerView.SpawnBullet(bullet); 
            }       
        }
        EventHandler.Instance.InvokeOnBulletFired();
    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _playerView._shieldsPrefab.SetActive(false);
            return;
        }
        _lives--;
        if(_lives == 2)
        {
            CameraShaker.Instance.CameraShake(0.5f, 0.15f);
            _playerView._leftWing.SetActive(true);
        }
        else if(_lives == 1)
        {
            CameraShaker.Instance.CameraShake(0.5f, 0.15f);
            _playerView._rightWing.SetActive(true);
        }

        UIManager.Instance.UpdateLives(_lives);

        if(_lives < 1)
        {
            CameraShaker.Instance.CameraShake(0.5f, 0.15f);
            SpawnManager.Instance.OnPlayerDeath();
            UIManager.Instance.UpdateHighScore();
            _playerView.DestroyShip();
            UnSubscribeEvents();
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        AudioManager.Instance.PlayPowerUpAudio();
        _playerView.TripleShotTimer();
    }

    public void SpeedBoostActive()
    {
        _playerModel._speed *= _playerModel._speedBoostMultiplier;
        AudioManager.Instance.PlayPowerUpAudio();
        _playerView.SpeedBoostTimer();
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        AudioManager.Instance.PlayPowerUpAudio();
        _playerView._shieldsPrefab.SetActive(true);
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        UIManager.Instance.UpdateScore(_score);
    }
}
