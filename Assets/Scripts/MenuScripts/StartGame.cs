using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameButton()
    {
        // Delete all saved data for a fresh start
        PlayerPrefs.DeleteAll();

        // Clear coins and global upgrades
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.totalCoins = 0;
            CoinManager.Instance.SaveCoins(); // Ensure coins are reset in persistent storage
        }

        if (GlobalUpgrades.Instance != null)
        {
            GlobalUpgrades.Instance.ResetUpgrades(); // Clear all global upgrades
        }

        // Load the first scene (no saved checkpoints since we cleared)
        SceneManager.LoadScene("scene1");
    }

    public void LoadGameButton()
    {
        // Check if a checkpoint has been saved
        if (PlayerPrefs.HasKey("CheckpointScene"))
        {
            string checkpointScene = PlayerPrefs.GetString("CheckpointScene");
            // Load that scene, player position will be handled by the GameManager
            SceneManager.LoadScene(checkpointScene);
        }
        else
        {
            // No saved checkpoint, load the default starting scene
            SceneManager.LoadScene("scene1");
        }
    }

    public void QuitGameButton()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
