using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_OnKeyPress : MonoBehaviour
{
    [SerializeField] private GameObject uiElement;        // UI element (e.g. "Press F to pick up")
    [SerializeField] private bool isBossTrigger = false;  // If true, this is the boss door/trigger
    [SerializeField] private AudioClip pickupSound;       // Assign in Inspector

    private keyCounter kc;             // Reference to our keyCounter script
    private bool isPlayerNearby = false;

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
                // Play pickup sound
                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                kc.IncrementKeyCount();

                // Remove the key object and its UI prompt
                Destroy(transform.parent.gameObject);
                if (uiElement != null)
                    Destroy(uiElement);
            }
            else
            {
                // Boss door logic
                if (kc.keyCount == kc.GetTotalKeysNeeded())
                {
                    kc.keyCount = 0; // Reset key count, if that's your game logic
                    SceneManager.LoadScene("Battle"); 
                }
                else
                {
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
