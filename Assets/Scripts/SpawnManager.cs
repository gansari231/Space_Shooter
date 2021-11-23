using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemyCoroutine");
        StartCoroutine("SpawnPowerupCoroutine");
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(2.0f);

        while(_stopSpawning == false)
        {
            float randomEnemySpawn = Random.Range(3.0f, 5.0f);
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(randomEnemySpawn);
        }
    }

    IEnumerator SpawnPowerupCoroutine()
    {
        yield return new WaitForSeconds(2.0f);

        while (_stopSpawning == false)
        {
            Vector3 powerupPosSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0);
            int powerupIndex = Random.Range(0, _powerups.Length);
            Instantiate(_powerups[powerupIndex], powerupPosSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        } 
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}