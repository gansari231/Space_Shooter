using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    BossGun[] _guns;
    float _canFire = -1;
    float _fireRate = 0.5f;
    float _speed = 3f;
    int _amplitude = 1;

    void Start()
    {
        _guns = transform.GetComponentsInChildren<BossGun>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= 4.0f)
        {
            float x = (Mathf.Cos(Time.time) * _amplitude);
            float y = 4.0f;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);
        }

        if(Time.time > _canFire)
        {
            ActivateBossFireBall();
        }
    }

    void ActivateBossFireBall()
    {
        _canFire = Time.time + _fireRate;
        foreach(var gun in _guns)
        {
            gun.Shoot();
        }
    }
}
