using UnityEngine;

using UnityEngine.UI;

public class HUDStateManager : MonoBehaviour
{
    [Header("Referencias del HUD")]
    public Image iconoEstadoUI;

    [Header("Sprites de Estado")]
    public Sprite spriteVacio;   // ¡Nuevo! Arrastra aquí el marco vacío.
    public Sprite spriteAngel;
    public Sprite spriteDemonio;

    private void Awake()
    {
        // Intenta auto-asignar la referencia si está vacía
        if (iconoEstadoUI == null)
        {
            iconoEstadoUI = GetComponent<Image>();
        }
    }

    private void Start()
    {
        // Al empezar, nos aseguramos de que el icono esté vacío.
        if (iconoEstadoUI != null && spriteVacio != null)
        {
            iconoEstadoUI.sprite = spriteVacio;
        }
    }

    private void OnEnable()
    {
        StateManager.OnStateChanged += CambiarIcono;
    }

    private void OnDisable()
    {
        StateManager.OnStateChanged -= CambiarIcono;
    }

    private void CambiarIcono(StateManager.PlayerState nuevoEstado)
    {
        if (iconoEstadoUI == null) return;

        switch (nuevoEstado)
        {
            case StateManager.PlayerState.Angel:
                iconoEstadoUI.sprite = spriteAngel;
                break;
            case StateManager.PlayerState.Demonio:
                iconoEstadoUI.sprite = spriteDemonio;
                break;
                // Podrías añadir un estado 'Default' aquí si el StateManager lo soporta.
        }
    }
}