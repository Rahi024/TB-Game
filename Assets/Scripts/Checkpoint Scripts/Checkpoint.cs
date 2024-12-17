using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player triggers the checkpoint
        {
            // Save checkpoint data
            PlayerPrefs.SetString("CheckpointScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetFloat("CheckpointPosX", transform.position.x);
            PlayerPrefs.SetFloat("CheckpointPosY", transform.position.y);
            PlayerPrefs.SetFloat("CheckpointPosZ", transform.position.z);

            PlayerPrefs.Save(); // Ensure data is saved
            Debug.Log("Checkpoint Saved: " + SceneManager.GetActiveScene().name +
                      " at Position: " + transform.position);
        }
    }
}