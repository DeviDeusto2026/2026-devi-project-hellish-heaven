using UnityEngine;
using System.Collections.Generic;

public class Arrow : MonoBehaviour
{
    private float arrowDamage;
    private List<GameObject> hitEnemies = new List<GameObject>();
    public void configureDamage(float bowDamage)
    {
        arrowDamage = bowDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo") && !hitEnemies.Contains(other.gameObject))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.ReceiveDamage(arrowDamage);
                hitEnemies.Add(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}