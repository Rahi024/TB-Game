using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverUI;

    void Update()
    {
        // Unlock and make the cursor visible when game is over
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        GameIsOver = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game speed

        if (PlayerPrefs.HasKey("CheckpointScene"))
        {
            // Load the saved checkpoint scene
            string checkpointScene = PlayerPrefs.GetString("CheckpointScene");
            SceneManager.LoadScene(checkpointScene);
        }
        else
        {
            // No checkpoint exists, reload the default scene
            Debug.LogWarning("No checkpoint found. Restarting default scene.");
            SceneManager.LoadScene("Scene1");
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
