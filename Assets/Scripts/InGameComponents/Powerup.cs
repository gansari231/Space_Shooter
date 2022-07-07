using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 2.0f;
    [SerializeField]    //0 --> Triple Shot, 1 --> Speed Boost, 2 --> Shield PowerUp
    private int powerupID;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerView _playerView = other.transform.GetComponent<PlayerView>();
            if (_playerView != null)
            {
                switch(powerupID)
                {
                    case 0:
                        _playerView._playerController.TripleShotActive();
                        break;

                    case 1:
                        _playerView._playerController.SpeedBoostActive();
                        break;

                    case 2:
                        _playerView._playerController.ShieldsActive();
                        break;

                    default:
                        Debug.Log("Default Value.");
                        break;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
