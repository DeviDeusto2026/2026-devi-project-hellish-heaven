using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigo;
    public float intervalo = 2.0f;
    private float cronometro = 0f;
    public Transform puntoSpawn;
    public float radioCuadrado = 3f;

    void Update()
    {
        cronometro += Time.deltaTime;
        if (cronometro >= intervalo)
        {
            SpawnEnemigo();
            cronometro = 0f;
        }
    }

    void SpawnEnemigo()
    {
        float offsetX = Random.Range(-radioCuadrado, radioCuadrado);
        float offsetZ = Random.Range(-radioCuadrado, radioCuadrado);

        Vector3 posicionAleatoria = new Vector3(
            puntoSpawn.position.x + offsetX,
            puntoSpawn.position.y, 
            puntoSpawn.position.z + offsetZ
        );

        Instantiate(enemigo, posicionAleatoria, puntoSpawn.rotation);
    }
}