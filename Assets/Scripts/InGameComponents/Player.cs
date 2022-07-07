using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldsPrefab;
    [SerializeField]
    private GameObject _rightWing;
    [SerializeField]
    private GameObject _leftWing;

    private float _speed = 5.0f;
    private float _speedBoostMultiplier = 2.0f;
    private float _fireRate = 0.2f;
    private float _canFire = -1.0f;

    private int _lives = 3;
    private int _score = 0;

    private SpawnManager _spawnManager;
    private UIManager _uIManager;

    private bool _isTripleShotActive = false;
    private bool _isShieldsActive = false;
    public bool _isPlayerOne = false;
    public bool _isPlayerTwo = false;

    private AudioSource _laserAudio;
    private AudioSource _powerUpAudio;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _laserAudio = GameObject.Find("Laser").GetComponent<AudioSource>();
        _powerUpAudio = GameObject.Find("Powerup").GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL.");
        }

        if(_uIManager == null)
        {
            Debug.LogError("UIManager is NULL.");
        }
    }

    void Update()
    {  
        if(_isPlayerOne == true)
        {
            PlayerOneMovement();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                PlayerOneFireLaser();
            }
        }
        else
        {
            PlayerTwoMovement();
            if (Input.GetKeyDown(KeyCode.RightShift) && Time.time > _canFire)
            {
                PlayerTwoFireLaser();
            }
        }
        SetPlayerBounds();
    }

    void PlayerOneMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");

        //changing players movement in more optimized way
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);   
    }

    void PlayerTwoMovement()
    {
        if(Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad2))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
    }

    void SetPlayerBounds()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }

    void PlayerOneFireLaser()
    {
        _canFire = Time.time + _fireRate;
        _laserAudio.Play();
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 offset = transform.position + new Vector3(0, 1.2f, 0);
            Instantiate(_laserPrefab, offset, Quaternion.identity);
        }
    }

    void PlayerTwoFireLaser()
    {
        _canFire = Time.time + _fireRate;
        _laserAudio.Play();
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 offset = transform.position + new Vector3(0, 1.2f, 0);
            Instantiate(_laserPrefab, offset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldsPrefab.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _leftWing.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightWing.SetActive(true);
        }

        _uIManager.UpdateLives(_lives);

        if(_lives < 1)
        {   
            _spawnManager.OnPlayerDeath();
            _uIManager.UpdateHighScore();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        _powerUpAudio.Play();
        StartCoroutine("TripleShotCoolDown");
    }

    IEnumerator TripleShotCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speed *= _speedBoostMultiplier;
        _powerUpAudio.Play();
        StartCoroutine("SpeedBoostCooldown");
    }

    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedBoostMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _powerUpAudio.Play();
        _shieldsPrefab.SetActive(true);
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _uIManager.UpdateScore(_score);
    }
}
