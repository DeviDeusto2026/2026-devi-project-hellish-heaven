using UnityEngine;

public class PlayerStateEffects : MonoBehaviour
{
    [Header("Bonus estado Demonio")]
    public float bonusVidaDemonio = 50f;  
    public float bonusManaDemonio = 50f; 

    [Header("Bonus estado Angel")]
    public float regenVidaAngel = 5f;   
    public float bonusDanyoAngel = 1.3f; // multiplicador de daño (30% más)

    private PlayerHealth _health;
    private PlayerMana _mana;

    void Awake()
    {
        _health = GetComponent<PlayerHealth>();
        _mana = GetComponent<PlayerMana>();
    }

    void OnEnable()
    {
        StateManager.OnStateChanged += OnStateChanged;
    }

    void OnDisable()
    {
        StateManager.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(StateManager.PlayerState nuevoEstado)
    {

        _health.AplicarBonusVida(0f);
        _health.AplicarRegenVida(0f);
        _mana.AplicarBonusMana(0f);


        switch (nuevoEstado)
        {
            case StateManager.PlayerState.Demon:
                _health.AplicarBonusVida(bonusVidaDemonio);
                _mana.AplicarBonusMana(bonusManaDemonio);
                Debug.Log("Demonio: +vida, +mana");
                break;

            case StateManager.PlayerState.Angel:
                _health.AplicarRegenVida(regenVidaAngel);
                Debug.Log("Angel: +regen vida");
                break;
        }
    }
}