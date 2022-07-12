using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    float _moveSpeed = 3f;
    public Vector3 fireBallDirection = new Vector3(0, 0, -1);
    
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        transform.Translate(fireBallDirection * _moveSpeed * Time.deltaTime);
        if(transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerService.Instance._playerController.Damage();
            Destroy(this.gameObject);
        }
    }
}
