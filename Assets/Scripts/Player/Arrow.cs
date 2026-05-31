using UnityEngine;
using System.Collections.Generic;

public class Arrow : MonoBehaviour
{
    private float arrowDamage;
    private List<GameObject> enemigosGolpeados = new List<GameObject>();
    public void ConfigurarDanyo(float danyoArco)
    {
        arrowDamage = danyoArco;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo") && !enemigosGolpeados.Contains(other.gameObject))
        {
            EnemyHealth saludEnemigo = other.GetComponent<EnemyHealth>();

            if (saludEnemigo != null)
            {
                saludEnemigo.RecibirDanyo(arrowDamage);
                enemigosGolpeados.Add(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}