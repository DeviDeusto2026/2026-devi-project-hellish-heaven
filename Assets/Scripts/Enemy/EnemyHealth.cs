using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float actualHealth;

    void Start()
    {
        actualHealth = maxHealth;
    }

    public void ReceiveDamage(float damageAmount)
    {
        actualHealth -= damageAmount;

        if (actualHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        FindFirstObjectByType<EnemySpawner>().EnemyDefeated();
        Destroy(gameObject);
    }
}
