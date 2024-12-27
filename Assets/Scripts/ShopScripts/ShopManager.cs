using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Setup")]
    public GameObject itemButtonPrefab;   // Prefab for the shop buttons
    public Transform itemsParent;         // Parent transform for the buttons
    public BattleSystem battleSystem;     // Reference to the BattleSystem

    [Header("UI")]
    public TMPro.TextMeshProUGUI playerCoinsText; // Display for player coins

    private void Start()
    {
        PopulateShop();
        UpdateCoinDisplay();
    }

    private void PopulateShop()
    {
        // Health Upgrade
        CreateShopItem("Increase Health", 5, null, "Health", 10);

        // Attack Upgrade
        CreateShopItem("Increase Attack", 1, null, "Attack", 5);

        // You can add more upgrades here in the future
        // e.g. CreateShopItem("Something Else", 200, someIcon, "Else", 5);
    }

    private void CreateShopItem(string name, int startingCost, Sprite icon, string type, int value)
    {
        GameObject newButton = Instantiate(itemButtonPrefab, itemsParent);
        ShopItemButton buttonScript = newButton.GetComponent<ShopItemButton>();
        if (buttonScript != null)
        {
            buttonScript.SetItem(name, startingCost, icon, type, value, this, battleSystem);
        }
    }

    public void UpdateCoinDisplay()
    {
        if (playerCoinsText != null)
        {
            playerCoinsText.text = "Coins: " + CoinManager.Instance.totalCoins;
        }
    }
}
