using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public float manaMaximo = 100f;
    private float manaActual;

    [Header("Configuración")]
    public float regeneracionPorSegundo = 5f;
    public Slider sliderMana;

    void Start()
    {
        manaActual = manaMaximo;
        ActualizarUI();
    }

    void Update()
    {
        if (manaActual < manaMaximo)
        {
            manaActual += regeneracionPorSegundo * Time.deltaTime;
            ActualizarUI();
        }
    }

    public bool ConsumirMana(float cantidad)
    {
        if (manaActual >= cantidad)
        {
            manaActual -= cantidad;
            ActualizarUI();
            return true;
        }
        else
        {
            Debug.Log("¡No tienes suficiente maná!");
            return false;
        }
    }

    void ActualizarUI()
    {
        if (sliderMana != null)
        {
            sliderMana.value = manaActual / manaMaximo;
        }
    }
}
