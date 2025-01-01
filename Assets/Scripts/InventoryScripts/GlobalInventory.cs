using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public string itemName;
    public Sprite itemIcon;
    public string upgradeType;
    public int upgradeValue;
}

public class GlobalInventory : MonoBehaviour
{
    public static GlobalInventory Instance; // Singleton instance

    public List<InventoryItemData> savedItems = new List<InventoryItemData>(); // Stores inventory items

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public void SaveInventory(List<InventoryItemData> items)
    {
        savedItems = new List<InventoryItemData>(items); // Deep copy the items
        Debug.Log("Inventory saved!");
    }

    public List<InventoryItemData> LoadInventory()
    {
        Debug.Log("Inventory loaded!");
        return new List<InventoryItemData>(savedItems); // Return a copy of the saved items
    }
}
