using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float vidaMaxima = 100f;
    private float vidaActual;
    private float _bonusVidaMaxima = 0f;


    [Header("UI")]
    public Slider hpSlider;

    [Header("Game Over Sistema")]
    public GameOverManager gameOverScreen;

    private bool isDead = false;

    public string escenaMenu = "Menu";

    void Start()
    {
        vidaActual = vidaMaxima;

        if (hpSlider != null)
        {
            hpSlider.minValue = 0f;
            hpSlider.maxValue = vidaMaxima;
            hpSlider.value = vidaActual;
        }
    }

    public void ReceiveDamage(float damageAmount)
    {
        if (isDead) return;

        vidaActual -= damageAmount;
        vidaActual = Mathf.Clamp(vidaActual, 0, maxHealth);
        UpdateUI();

        if (vidaActual <= 0)
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
            hpSlider.maxValue = vidaMaxima;
            hpSlider.value = vidaActual;
        }
    }

    public void AplicarBonusVida(float bonus)
    {
        vidaActual -= _bonusVidaMaxima;
        vidaMaxima -= _bonusVidaMaxima;
        _bonusVidaMaxima = bonus;
        vidaMaxima += _bonusVidaMaxima;
        vidaActual += _bonusVidaMaxima;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        if (hpSlider != null)
            hpSlider.maxValue = 1f;
        Debug.Log($"vidaActual: {vidaActual} | vidaMaxima: {vidaMaxima} | slider: {vidaActual / vidaMaxima}");
        UpdateUI();
    }

    private float _regenVida = 0f;

    public void AplicarRegenVida(float regenPorSegundo)
    {
        _regenVida = regenPorSegundo;
    }

    void Update()
    {
        if (_regenVida > 0f && vidaActual < vidaMaxima)
        {
            vidaActual += _regenVida * Time.deltaTime;
            vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
            UpdateUI();
        }
    }
}