using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float actualHp;

    [Header("UI")]
    public Slider HpSlider;

    [Header("Game Over Sistema")]
    public GameOverManager gameOverScreen;

    private bool isDead = false;

    void Start()
    {
        actualHp = maxHealth;
        UpdateUI();
    }

    public void ReceiveDamage(float damageAmount)
    {
        if (isDead) return;

        actualHp -= damageAmount;
        actualHp = Mathf.Clamp(actualHp, 0, maxHealth);
        UpdateUI();

        if (actualHp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;

        if (gameOverScreen != null)
        {
            gameOverScreen.ActivateGameOverPanel();
        }
        else
        {
            Debug.LogWarning("Falta asignar 'pantallaGameOver' en el inspector de PlayerHealth.");
        }
    }

    void UpdateUI()
    {
        if (HpSlider != null)
        {
            HpSlider.value = actualHp / maxHealth;
        }
    }
}