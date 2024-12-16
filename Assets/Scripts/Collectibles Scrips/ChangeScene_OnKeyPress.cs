using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_OnKeyPress : MonoBehaviour
{
    [SerializeField] private GameObject uiElement; // UI element to show when the player is nearby
    private keyCounter kc; // Reference to the keyCounter component

    private void Start()
    {
        // Find the keyCounter instance in the scene
        kc = FindObjectOfType<keyCounter>();

        // Optional: Check if kc is assigned correctly
        if (kc == null)
        {
            Debug.LogError("keyCounter instance not found in the scene!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiElement.SetActive(true); // Show the UI element
            
            // Check for key press and increment the key count
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (kc != null) // Check if kc is not null before calling the method
                {
                    kc.IncrementKeyCount(); // Call the method to increment key count

                    // Destroy the parent object (sphere) of this trigger
                    Destroy(transform.parent.gameObject); // This will destroy the parent object (sphere)
                    
                    // Optionally, destroy the UI element as well if you want
                    Destroy(uiElement); // This will destroy the UI element object
                }
            }

            // Check if the keyCount has reached 3 and load the new scene
            if (kc != null && kc.keyCount > 3)
            {
                kc.keyCount = 0; // Reset the key count
                SceneManager.LoadScene("Battle"); // Load the specified scene
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiElement.SetActive(false); // Hide the UI element
        }
    }
}
