using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Player prefab reference
    private GameObject playerInstance; // To store the reference of the player in the scene

    void Start()
    {
        // Check if a checkpoint exists
        if (PlayerPrefs.HasKey("CheckpointScene"))
        {
            string checkpointScene = PlayerPrefs.GetString("CheckpointScene");
            if (SceneManager.GetActiveScene().name == checkpointScene)
            {
                // Load the player's position
                Vector3 checkpointPos = new Vector3(
                    PlayerPrefs.GetFloat("CheckpointPosX"),
                    PlayerPrefs.GetFloat("CheckpointPosY"),
                    PlayerPrefs.GetFloat("CheckpointPosZ")
                );

                // Check if a player already exists
                playerInstance = GameObject.FindGameObjectWithTag("Player");
                if (playerInstance != null)
                {
                    // Move existing player to the checkpoint position
                    playerInstance.transform.position = checkpointPos;
                }
                else
                {
                    // Instantiate a new player if no existing one is found
                    playerInstance = Instantiate(playerPrefab, checkpointPos, Quaternion.identity);
                }

                Debug.Log("Loaded Player at Checkpoint!");
            }
        }
        else
        {
            // Spawn the player at default position if no checkpoint exists
            Vector3 defaultSpawn = Vector3.zero;
            playerInstance = GameObject.FindGameObjectWithTag("Player");

            if (playerInstance == null)
            {
                playerInstance = Instantiate(playerPrefab, defaultSpawn, Quaternion.identity);
            }

            Debug.Log("No checkpoint found. Loaded default spawn position.");
        }
    }
}
