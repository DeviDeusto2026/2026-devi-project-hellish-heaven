using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Oleada
    {
        public string nombre;
        public GameObject tipoEnemigo;
        public int cantidad;
        public float tiempoEntreSpawns;
    }

    public List<Oleada> oleadas;
    public Transform[] puntosDeSpawn;

    private int oleadaActual = 0;
    private int enemigosVivos = 0;

    void Start()
    {
        if (oleadas.Count > 0)
        {
            StartCoroutine(SpawnWave(oleadaActual));
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        Transform punto = puntosDeSpawn[Random.Range(0, puntosDeSpawn.Length)];
        GameObject enemigo = Instantiate(prefab, punto.position, punto.rotation);
        enemigosVivos++;
    }

    IEnumerator SpawnWave(int indice)
    {
        Debug.Log("Iniciando: " + oleadas[indice].nombre);
        Oleada wave = oleadas[indice];

        for (int i = 0; i < wave.cantidad; i++)
        {
            SpawnEnemy(wave.tipoEnemigo);
            yield return new WaitForSeconds(wave.tiempoEntreSpawns);
        }

    }

    public void EnemigoDerrotado()
    {
        enemigosVivos--;

        if (enemigosVivos <= 0)
        {
            enemigosVivos = 0;
            StopAllCoroutines();
            oleadaActual++;

            if (oleadaActual < oleadas.Count)
            {
                StartCoroutine(SpawnWave(oleadaActual));
            }
            else
            {
                Debug.Log("¡Nivel Completado!");
            }
        }
    }
    
}