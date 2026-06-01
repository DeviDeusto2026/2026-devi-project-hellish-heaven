using UnityEngine;
using UnityEngine.UI;

public class HUDStateManager : MonoBehaviour
{
    [Header("Referencias del HUD")]
    public Image stateIconUI;

    [Header("Sprites de Estado")]
    public Sprite noStateSprite;
    public Sprite angelSprite;
    public Sprite demonSprite;

    private void Awake() {
        if (stateIconUI == null)
        {
            stateIconUI = GetComponent<Image>();
        }
    }

    private void Start() {
        if (stateIconUI != null && noStateSprite != null)
        {
            stateIconUI.sprite = noStateSprite;
        }
    }

    private void OnEnable()
    {
        StateManager.OnStateChanged += ChangeIcon;
    }

    private void OnDisable()
    {
        StateManager.OnStateChanged -= ChangeIcon;
    }

    private void ChangeIcon(StateManager.PlayerState newState) {
        if (stateIconUI == null) return;

        switch (newState) {
            case StateManager.PlayerState.Angel:
                stateIconUI.sprite = angelSprite;
                break;
            case StateManager.PlayerState.Demon:
                stateIconUI.sprite = demonSprite;
                break;
        }
    }
}