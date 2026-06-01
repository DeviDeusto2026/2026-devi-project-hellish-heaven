using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Panel de la UI (El objeto que se activará)")]
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        Time.timeScale = 1f;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("[Game Over] ¡No has arrastrado el objeto 'Panel_GameOver' a la casilla del script!", this);
        }
    }

    public void ActivateGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}