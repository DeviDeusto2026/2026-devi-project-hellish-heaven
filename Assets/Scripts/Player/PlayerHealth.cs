using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;
    private float _bonusVidaMaxima = 0f;

    [Header("UI")]
    public Slider sliderVida;

    public string escenaMenu = "Menu";

    void Start()
    {
        vidaActual = vidaMaxima;

        if (sliderVida != null)
        {
            sliderVida.minValue = 0f;
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
        }
    }

    public void RecibirDanyo(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarUI();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }
    public void Morir()
    {
        Debug.Log("Jugador Muerto");
        SceneManager.LoadScene(escenaMenu);
    }

    void ActualizarUI()
    {
        if (sliderVida != null)
        {
            sliderVida.maxValue = vidaMaxima;
            sliderVida.value = vidaActual;
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
        if (sliderVida != null)
            sliderVida.maxValue = 1f;
        Debug.Log($"vidaActual: {vidaActual} | vidaMaxima: {vidaMaxima} | slider: {vidaActual / vidaMaxima}");
        ActualizarUI();
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
            ActualizarUI();
        }
    }
}