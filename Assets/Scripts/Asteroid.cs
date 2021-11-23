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
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _explosionAudio = GameObject.Find("Enemy").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            _explosionAudio.Play();
            Destroy(this.gameObject);
        }
    }
}
