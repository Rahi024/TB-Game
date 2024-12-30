using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite healthPotionIcon; // Assign the health potion icon in the Inspector
    public Sprite attackPotionIcon; // Assign the attack potion icon in the Inspector
    public GameObject healthPotionPrefab; // Assign the Health Potion prefab in the Inspector
    public GameObject attackPotionPrefab; // Assign the Attack Potion prefab in the Inspector
    public InventoryManager inventoryManager; // Reference to the InventoryManager script

    private bool isLooted = false; // Tracks whether the chest has been looted

    public void OpenChest()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not assigned to the Chest script.");
            return;
        }

        if (isLooted)
        {
            // Update the message to indicate the chest is empty
            inventoryManager.DisplayMessage("Chest is now empty.");
            Debug.Log("Chest is empty.");
            return;
        }

        // Example logic: randomly add either a health or attack potion
        int randomChoice = Random.Range(0, 2);

        // Determine which prefab and properties to use
        GameObject selectedPrefab = randomChoice == 0 ? healthPotionPrefab : attackPotionPrefab;
        Sprite itemIcon = randomChoice == 0 ? healthPotionIcon : attackPotionIcon;
        string itemName = randomChoice == 0 ? "Health Potion" : "Attack Potion";
        string upgradeType = randomChoice == 0 ? "Health" : "Attack";
        int upgradeValue = randomChoice == 0 ? 10 : 7;

        // Add the item to the inventory using the selected prefab
        inventoryManager.AddItemWithPrefab(selectedPrefab, itemIcon, itemName, upgradeType, upgradeValue);
        Debug.Log($"Added {itemName} to inventory.");

        // Update the message and mark the chest as looted
        inventoryManager.DisplayMessage($"Collected {itemName}!");
        isLooted = true; // Mark the chest as looted
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Press 'E' to open the chest.");
        }
    }

    private void Update()
    {
        // Open the chest when the player presses 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }
}
