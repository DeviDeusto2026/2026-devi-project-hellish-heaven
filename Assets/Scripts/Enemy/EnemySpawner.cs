using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public GameObject enemyType;
        public int enemyNumber;
        public float spawnCooldown;
    }

    public List<Wave> waves;
    public Transform[] spawnPoints;

    private int actualWave = 0;
    private int remainingEnemies = 0;

    void Start()
    {
        if (waves.Count > 0)
        {
            StartCoroutine(SpawnWave(actualWave));
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        remainingEnemies++;
    }

    IEnumerator SpawnWave(int index)
    {
        Debug.Log("Iniciando: " + waves[index].waveName);
        Wave wave = waves[index];

        for (int i = 0; i < wave.enemyNumber; i++)
        {
            SpawnEnemy(wave.enemyType);
            yield return new WaitForSeconds(wave.spawnCooldown);
        }

    }

    public void EnemyDefeated()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            remainingEnemies = 0;
            StopAllCoroutines();
            actualWave++;

            if (actualWave < waves.Count)
            {
                StartCoroutine(SpawnWave(actualWave));
            }
            else
            {
                Debug.Log("¡Nivel Completado!");
            }
        }
    }
}