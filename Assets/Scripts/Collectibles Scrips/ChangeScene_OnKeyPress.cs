using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_OnKeyPress : MonoBehaviour
{
    [SerializeField] private GameObject uiElement; // UI element (e.g. "Press F to pick up") shown when near
    [SerializeField] private bool isBossTrigger = false; // If true, this is the boss door/trigger
    private keyCounter kc; // Reference to our keyCounter script
    private bool isPlayerNearby = false; // Tracks if the player is in range

    private void Start()
    {
        // Find the keyCounter in the scene
        kc = FindObjectOfType<keyCounter>();
        if (kc == null)
        {
            Debug.LogError("keyCounter instance not found in the scene!");
        }

        // Ensure UI prompt is off at start
        if (uiElement != null) 
            uiElement.SetActive(false);
    }

    private void Update()
    {
        // If player is nearby and presses F, handle interaction
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            HandleInteraction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (uiElement != null)
                uiElement.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (uiElement != null)
                uiElement.SetActive(false);
        }
    }

    // Called when the player presses F while in range
    private void HandleInteraction()
    {
        if (kc != null)
        {
            // If this trigger is just a key pickup
            if (!isBossTrigger)
            {
                kc.IncrementKeyCount();

                // Remove the key object and its UI prompt
                Destroy(transform.parent.gameObject);
                if (uiElement != null) 
                    Destroy(uiElement);
            }
            else
            {
                // If it is a boss trigger, compare the collected keys vs. the needed keys
                if (kc.keyCount == kc.GetTotalKeysNeeded())
                {
                    // All keys collected, proceed
                    kc.keyCount = 0; // Reset key count, if that's your game logic
                    SceneManager.LoadScene("Battle"); // Or whatever your boss scene is called
                }
                else
                {
                    // Not enough keys
                    Debug.Log($"Collect all {kc.GetTotalKeysNeeded()} keys before accessing the boss!");
                }
            }
        }
        else
        {
            Debug.LogError("No keyCounter reference found!");
        }
    }
}
