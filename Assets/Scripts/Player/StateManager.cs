using System;
using UnityEngine;

public class StateManager: MonoBehaviour
{
    public enum PlayerState { Normal, Angel, Demonio }
    public PlayerState estadoActual = PlayerState.Normal;
    public static event Action<PlayerState> OnStateChanged;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AlternarEstado();
        }
    }

    void AlternarEstado() {
        if (estadoActual == PlayerState.Angel)
            estadoActual = PlayerState.Demonio;
        else
            estadoActual = PlayerState.Angel;
        OnStateChanged?.Invoke(estadoActual);
        Debug.Log("Estado actual: " + estadoActual);
    }
}
