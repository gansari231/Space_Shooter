using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3.0f;
    private float xSpawnPos = 8.5f;
    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;
    private Player _player;
    private Animator _enemyDestroyedAnim;

    private AudioSource _enemyExplosionAudio;

    [SerializeField]
    private GameObject _laserPrefab;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }

        _enemyDestroyedAnim = gameObject.GetComponent<Animator>();

        if (_enemyDestroyedAnim == null)
        {
            Debug.LogError("Animator is NULL.");
        }

        _enemyExplosionAudio = GameObject.Find("Enemy").GetComponent<AudioSource>();
    }

    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject _enemyLaser = Instantiate(_laserPrefab, (transform.position + new Vector3(0 , -0.7f, 0)), Quaternion.identity);
            Laser[] _lasers = _enemyLaser.GetComponentsInChildren<Laser>();

            for(int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5)
        {
            float randomX = Random.Range(-xSpawnPos, xSpawnPos);
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _enemyExplosionAudio.Play();
            Destroy(this.gameObject, 2.4f);
        }

        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            _enemyDestroyedAnim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _enemyExplosionAudio.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.4f);
        }
    }
}
