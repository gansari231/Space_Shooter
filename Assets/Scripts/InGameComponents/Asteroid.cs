using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotateSpeed = 15.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    private AudioSource _explosionAudio;

    void Start()
    {
        _explosionAudio = GameObject.Find("Enemy").GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            SpawnManager.Instance.StartSpawning();
            _explosionAudio.Play();
            Destroy(this.gameObject);
        }
    }
}
