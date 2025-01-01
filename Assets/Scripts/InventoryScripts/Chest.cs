using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite healthPotionIcon;
    public Sprite attackPotionIcon;
    public GameObject healthPotionPrefab;
    public GameObject attackPotionPrefab;
    public InventoryManager inventoryManager;

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
        isLooted = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Player is near the chest
            Debug.Log("Press 'E' to open the chest.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player has moved away from the chest
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
}
