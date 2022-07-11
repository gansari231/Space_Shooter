using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    [SerializeField]
    Fireball _fireBall;
    Vector3 _direction;

    Fireball fireball;

    private void Update()
    {
        _direction = (transform.localRotation * -transform.up).normalized;    
    }

    public void Shoot()
    {
        fireball = _fireBall.GetComponent<Fireball>();
        fireball.fireBallDirection = _direction;
        GameObject newFireBallObj = Instantiate(_fireBall.gameObject, transform.position, Quaternion.identity);     
    }
}
