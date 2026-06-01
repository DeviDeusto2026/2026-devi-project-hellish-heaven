using System;
using UnityEngine;

public class StateManager: MonoBehaviour
{
    public enum PlayerState { Normal, Angel, Demon }
    public PlayerState actualState = PlayerState.Normal;
    public static event Action<PlayerState> OnStateChanged;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeState();
        }
    }

    void ChangeState() {
        if (actualState == PlayerState.Angel)
            actualState = PlayerState.Demon;
        else
            actualState = PlayerState.Angel;
        OnStateChanged?.Invoke(actualState);
        Debug.Log("Estado actual: " + actualState);
    }
}
