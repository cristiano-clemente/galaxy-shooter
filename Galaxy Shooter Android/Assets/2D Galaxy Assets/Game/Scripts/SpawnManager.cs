using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;

    private Coroutine _enemySpawn;
    private Coroutine _powerupSpawn;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
    }

    public void StartSpawnRoutines()
    {
        _enemySpawn = StartCoroutine(SpawnEnemy());
        _powerupSpawn = StartCoroutine(SpawnPowerup());
    }

    public void StopSpawnRoutines()
    {
        StopCoroutine(_enemySpawn);
        StopCoroutine(_powerupSpawn);
    }

    public IEnumerator SpawnEnemy()
    {
        while (_gameManager.gameOver == false)
        {
            float randomX = Random.Range(-7.76f, 7.76f);
            Instantiate(enemyPrefab, new Vector3(randomX, 6.44f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    public IEnumerator SpawnPowerup()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(randomPowerup, 6.44f, 0), Quaternion.identity);
            yield return new WaitForSeconds(7.0f);
        }
    }
}
