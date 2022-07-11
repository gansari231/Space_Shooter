using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Wave
{
    public int enemyCount;
    public float timeBetweenSpawns;
    public GameObject _enemyPrefabs;
}

public class SpawnManager : SingletonGeneric<SpawnManager>
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    private bool _stopSpawning = false;
    bool _isAsteroidDestroyed;

    public Wave[] waves;
    Wave currentWave;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    private void Update()
    {
        if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime && _isAsteroidDestroyed)
        {
            SpawnEnemy();
        }
    }

    public void StartSpawning()
    {
        _isAsteroidDestroyed = true;
        StartCoroutine(SpawnPowerupCoroutine());
        StartCoroutine(NextWave());
    }

    private void SpawnEnemy()
    {
        enemiesRemainingToSpawn--;
        nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
        Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-8.5f, 8.5f), 7.5f, 0);
        GameObject newEnemy = Instantiate(currentWave._enemyPrefabs, posToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        Enemy.onDeath = OnEnemyDeath;
    }

    IEnumerator SpawnPowerupCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 powerupPosSpawn = new Vector3(UnityEngine.Random.Range(-8.5f, 8.5f), 7.5f, 0);
            int powerupIndex = UnityEngine.Random.Range(0, _powerups.Length);
            Instantiate(_powerups[powerupIndex], powerupPosSpawn, Quaternion.identity);
            yield return new WaitForSeconds(UnityEngine.Random.Range(5.0f, 8.0f));
        } 
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(4.0f);
        currentWaveNumber++;
        if(currentWaveNumber - 1 < waves.Length)
        {
            UIManager.Instance.UpdateWavetext(currentWaveNumber);
            currentWave = waves[currentWaveNumber - 1];
            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        if(enemiesRemainingAlive == 0)
        {
            StartCoroutine(NextWave());
        }
    }
}