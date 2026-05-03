using System;
using UnityEngine;

public class StateManager: MonoBehaviour
{
    // Definimos los estados posibles
    public enum PlayerState { Normal, Angel, Demonio }
    public PlayerState estadoActual = PlayerState.Normal;

    // Evento que avisará a otros scripts cuando cambiemos de estado
    public static event Action<PlayerState> OnStateChanged;

    void Update()
    {
        // Cambiamos de estado con la tecla 'Tab' (por ejemplo)
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AlternarEstado();
        }
    }

    void AlternarEstado()
    {
        if (estadoActual == PlayerState.Angel)
            estadoActual = PlayerState.Demonio;
        else
            estadoActual = PlayerState.Angel;

        // Disparamos el evento para que el resto del juego se entere
        OnStateChanged?.Invoke(estadoActual);

        Debug.Log("Estado actual: " + estadoActual);
    }
}
