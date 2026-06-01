using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.ReceiveDamage(10f);
            }
        }
    }
}
