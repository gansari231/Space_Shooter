using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    GameObject[] _enemies;
    GameObject _closestEnemy;
    float _closestEnemyDistance;

    bool _isClosestEnemyFound;
    Rigidbody2D _rb;
    float _speed = 5f;
    float _rotationSpeed = 200f;
    
    void Start()
    {
        _isClosestEnemyFound = false;
        _rb = GetComponent<Rigidbody2D>();
        _closestEnemyDistance = Mathf.Infinity;
        _closestEnemy = null;
    }

    void Update()
    {
        GetClosestEnemy();
    }

    void FixedUpdate()
    {
        MoveToClosestEnemy();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void GetClosestEnemy()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in _enemies)
        {
            float _currentDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if(_currentDistance < _closestEnemyDistance)
            {
                _closestEnemyDistance = _currentDistance;
                _closestEnemy = enemy;
                _isClosestEnemyFound = true;
            }
        }
    }

    void MoveToClosestEnemy()
    {
        if(_closestEnemy != null)
        {
            if(_isClosestEnemyFound)
            {
                Vector2 _direction = (Vector2)_closestEnemy.transform.position - (Vector2)transform.position;
                _direction.Normalize();
                float _crossValue = Vector3.Cross(_direction, transform.up).z;

                if(_rb != null)
                {
                    _rb.angularVelocity = _crossValue * -_rotationSpeed;
                    _rb.velocity = transform.up * _speed;
                }
                else
                {
                    _rb.velocity = transform.up * _speed;
                    if(_rb.position.y > 15f)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
