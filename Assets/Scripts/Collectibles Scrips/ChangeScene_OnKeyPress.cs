using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_OnKeyPress : MonoBehaviour
{
    [SerializeField] private GameObject uiElement; // UI element to show when the player is nearby
    [SerializeField] private bool isBossTrigger = false; // Flag to identify the boss trigger
    private keyCounter kc; // Reference to the keyCounter component
    private bool isPlayerNearby = false; // Tracks if the player is near the key or trigger

    private void Start()
    {
        kc = FindObjectOfType<keyCounter>();
        if (kc == null)
        {
            Debug.LogError("keyCounter instance not found in the scene!");
        }
    }

    private void Update()
    {
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
            uiElement.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            uiElement.SetActive(false);
        }
    }

    private void HandleInteraction()
    {
        if (kc != null)
        {
            if (!isBossTrigger)
            {
                kc.IncrementKeyCount();
                Destroy(transform.parent.gameObject);
                Destroy(uiElement);
            }
            else
            {
                if (kc.keyCount == 3)
                {
                    kc.keyCount = 0;
                    // Transition to the Battle scene
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
                }
                else
                {
                    Debug.Log("Collect all 3 keys before accessing the boss!");
                }
            }
        }
    }
}
