using UnityEngine;
using TMPro; // Required for TextMeshPro components

public class Chest : MonoBehaviour
{
    public Sprite healthPotionIcon;
    public Sprite attackPotionIcon;
    public GameObject healthPotionPrefab;
    public GameObject attackPotionPrefab;
    public InventoryManager inventoryManager;
    public AudioSource chestAudioSource; // Reference to the AudioSource component

    [Header("UI Elements")]
    public TMP_Text interactionPrompt; // TextMeshPro element to display messages (drag and drop in Inspector)

    private bool isLooted = false; // Tracks whether the chest has been looted
    private bool isPlayerNearby = false; // Tracks whether the player is near the chest

    public void OpenChest()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not assigned to the Chest script.");
            return;
        }

        if (isLooted)
        {
            inventoryManager.DisplayMessage("Chest is now empty.");
            Debug.Log("Chest is empty.");
            return;
        }

        int randomChoice = Random.Range(0, 2);

        GameObject selectedPrefab = randomChoice == 0 ? healthPotionPrefab : attackPotionPrefab;
        Sprite itemIcon = randomChoice == 0 ? healthPotionIcon : attackPotionIcon;
        string itemName = randomChoice == 0 ? "Health Potion" : "Attack Potion";
        string upgradeType = randomChoice == 0 ? "Health" : "Attack";
        int upgradeValue = randomChoice == 0 ? 10 : 7;

        inventoryManager.AddItemWithPrefab(selectedPrefab, itemIcon, itemName, upgradeType, upgradeValue);
        Debug.Log($"Added {itemName} to inventory.");

        inventoryManager.DisplayMessage($"Collected {itemName}!");

        // Play the chest opening sound effect
        if (chestAudioSource != null)
        {
            chestAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Chest AudioSource is not assigned.");
        }

        isLooted = true;
        HideInteractionPrompt(); // Hide the interaction prompt after looting
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is near the chest
            ShowInteractionPrompt("Press 'E' to open the chest");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player has moved away from the chest
            HideInteractionPrompt();
        }
    }

    private void Update()
    {
        // Open the chest only if the player is nearby and presses 'E'
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    /// <summary>
    /// Displays the interaction prompt with the specified message.
    /// </summary>
    private void ShowInteractionPrompt(string message)
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(true);
            interactionPrompt.text = message;
        }
        else
        {
            Debug.LogWarning("Interaction prompt is not assigned.");
        }
    }

    /// <summary>
    /// Hides the interaction prompt.
    /// </summary>
    private void HideInteractionPrompt()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }
}
