using UnityEngine;
using UnityEngine.UI; // Required for UI components like Button and Image
using TMPro; // Required for TextMeshPro components
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI; // The inventory UI GameObject to activate/deactivate
    public GameObject inventoryContent; // The Content container in the Scroll View
    public GameObject defaultItemPrefab; // Default prefab for inventory items
    public TMP_Text messageDisplay; // TextMeshPro Text element to display messages (drag and drop in Inspector)

    // References to player and camera movement scripts
    public MonoBehaviour playerMovementScript; // Assign your player movement script here
    public MonoBehaviour cameraMovementScript; // Assign your camera movement script here

    private List<InventoryItemData> items = new List<InventoryItemData>(); // Stores inventory items

    private void Start()
    {
        // Load inventory if there are saved items
        if (GlobalInventory.Instance != null && GlobalInventory.Instance.savedItems.Count > 0)
        {
            items = GlobalInventory.Instance.LoadInventory();
            RepopulateInventoryUI();
        }
    }

    /// <summary>
    /// Adds a new item to the inventory and creates its UI element.
    /// </summary>
    public void AddItemWithPrefab(GameObject prefab, Sprite itemIcon, string itemName, string upgradeType = null, int upgradeValue = 0)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned for the item.");
            return;
        }

        // Create the new item data
        InventoryItemData newItem = new InventoryItemData
        {
            itemName = itemName,
            itemIcon = itemIcon,
            upgradeType = upgradeType,
            upgradeValue = upgradeValue
        };
        items.Add(newItem); // Add to the inventory list

        // Create the UI element for the new item
        CreateItemUI(newItem);
    }

    /// <summary>
    /// Creates the UI element for a given inventory item.
    /// </summary>
    private void CreateItemUI(InventoryItemData item)
    {
        // Instantiate the prefab
        GameObject newItemUI = Instantiate(defaultItemPrefab, inventoryContent.transform);

        // Set the item's icon
        Image itemImage = newItemUI.transform.Find("Image").GetComponent<Image>();
        if (itemImage != null)
            itemImage.sprite = item.itemIcon;

        // Set the item's name using TextMeshPro
        TMP_Text itemText = newItemUI.transform.Find("ItemName").GetComponent<TMP_Text>();
        if (itemText != null)
            itemText.text = item.itemName;

        // Add functionality to the item's button for using the item
        Button itemButton = newItemUI.GetComponent<Button>();
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() => UseItem(item, newItemUI));
        }

        // Add functionality to the remove button
        Button removeButton = newItemUI.transform.Find("RemoveButton").GetComponent<Button>();
        if (removeButton != null)
        {
            removeButton.onClick.AddListener(() => RemoveItem(newItemUI, item));
        }
    }

    /// <summary>
    /// Uses an item, applies its upgrade, and removes it from the inventory.
    /// </summary>
    public void UseItem(InventoryItemData itemData, GameObject itemUI)
    {
        Debug.Log("UseItem triggered for: " + itemData.itemName);

        // Apply the upgrade to GlobalUpgrades
        ApplyUpgrade(itemData.upgradeType, itemData.upgradeValue);

        // Display a message about the upgrade
        DisplayMessage($"Used {itemData.itemName}: {itemData.upgradeType} increased by {itemData.upgradeValue}!");

        // Remove the item from inventory after use
        RemoveItem(itemUI, itemData);
    }

    /// <summary>
    /// Applies the upgrade based on the item's upgrade type and value.
    /// </summary>
    private void ApplyUpgrade(string upgradeType, int upgradeValue)
    {
        if (GlobalUpgrades.Instance == null)
        {
            Debug.LogError("GlobalUpgrades instance is not initialized!");
            return;
        }

        switch (upgradeType)
        {
            case "Health":
                GlobalUpgrades.Instance.healthBonus += upgradeValue;
                Debug.Log($"HealthBonus updated: {GlobalUpgrades.Instance.healthBonus}");
                break;

            case "Attack":
                GlobalUpgrades.Instance.attackBonus += upgradeValue;
                Debug.Log($"AttackBonus updated: {GlobalUpgrades.Instance.attackBonus}");
                break;

            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    /// <summary>
    /// Removes an item from the inventory and updates the UI.
    /// </summary>
    public void RemoveItem(GameObject itemUI, InventoryItemData itemData)
    {
        if (itemUI != null) Destroy(itemUI);
        if (itemData != null)
        {
            bool removed = items.Remove(itemData);
            if (removed)
            {
                Debug.Log($"Removed item: {itemData.itemName}");
                SaveInventory(); // Save the inventory after removal
            }
            else
            {
                Debug.LogWarning($"Attempted to remove item that does not exist: {itemData.itemName}");
            }
        }
    }

    /// <summary>
    /// Displays a temporary message on the UI.
    /// </summary>
    public void DisplayMessage(string message)
    {
        if (messageDisplay != null)
        {
            // Enable the text and set the message
            messageDisplay.gameObject.SetActive(true);
            messageDisplay.text = message;

            // Clear the message and disable the text after a short delay
            CancelInvoke("ClearMessage");
            Invoke("ClearMessage", 2f); // Adjust delay as needed
        }
        else
        {
            Debug.LogWarning("Message display is not assigned in the Inspector.");
        }
    }

    /// <summary>
    /// Clears the displayed message.
    /// </summary>
    public void ClearMessage()
    {
        if (messageDisplay != null)
        {
            // Clear the text and disable the message display
            messageDisplay.text = "";
            messageDisplay.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the inventory UI on or off.
    /// </summary>
    public void ToggleInventory(bool isOpen)
    {
        if (inventoryUI == null)
        {
            Debug.LogError("Inventory UI GameObject is not assigned.");
            return;
        }

        if (isOpen)
        {
            // Open inventory
            inventoryUI.SetActive(true); // Enable the inventory UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (playerMovementScript != null) playerMovementScript.enabled = false;
            if (cameraMovementScript != null) cameraMovementScript.enabled = false;

            Debug.Log("Inventory opened. Screen movement disabled.");
        }
        else
        {
            // Close inventory
            inventoryUI.SetActive(false); // Disable the inventory UI
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (playerMovementScript != null) playerMovementScript.enabled = true;
            if (cameraMovementScript != null) cameraMovementScript.enabled = true;

            Debug.Log("Inventory closed. Screen movement enabled.");
        }
    }

    /// <summary>
    /// Saves the current inventory to the GlobalInventory instance.
    /// </summary>
    public void SaveInventory()
    {
        if (GlobalInventory.Instance != null)
        {
            GlobalInventory.Instance.SaveInventory(items);
        }
        else
        {
            Debug.LogError("GlobalInventory instance is not initialized!");
        }
    }

    private void OnDestroy()
    {
        // Save inventory when the scene is destroyed
        SaveInventory();
    }

    /// <summary>
    /// Repopulates the inventory UI based on the current items list.
    /// </summary>
    private void RepopulateInventoryUI()
    {
        // Clear existing UI
        foreach (Transform child in inventoryContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Repopulate UI with saved items
        foreach (var item in items)
        {
            CreateItemUI(item);
        }
    }
}
