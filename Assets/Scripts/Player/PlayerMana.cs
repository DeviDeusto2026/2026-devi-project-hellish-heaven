using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public float maxMana = 100f;
    private float actualMana;

    [Header("Configuración")]
    public float regenerationPerSecond = 5f;
    public Slider manaSlider;

    void Start()
    {
        actualMana = maxMana;
        UpdateUI();
    }

    void Update()
    {
        if (actualMana < maxMana)
        {
            actualMana += regenerationPerSecond * Time.deltaTime;
            UpdateUI();
        }
    }

    public bool ConsumeMana(float manaConsumption)
    {
        if (actualMana >= manaConsumption)
        {
            actualMana -= manaConsumption;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("¡No tienes suficiente maná!");
            return false;
        }
    }

    void UpdateUI()
    {
        if (manaSlider != null)
        {
            manaSlider.value = actualMana / maxMana;
        }
    }
}
