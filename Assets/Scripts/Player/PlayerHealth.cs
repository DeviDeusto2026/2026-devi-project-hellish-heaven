using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;

    [Header("UI")]
    public Slider sliderVida;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarUI();
    }

    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarUI();

        if (vidaActual <= 0)
        {
            Debug.Log("Jugador Muerto");
        }
    }

    void ActualizarUI()
    {
        if (sliderVida != null)
        {
            sliderVida.value = vidaActual / vidaMaxima;
        }
    }
}