using UnityEngine;
using TMPro; // for TextMeshProUGUI

public class ShopManager : MonoBehaviour
{
    [Header("Shop Setup")]
    public GameObject itemButtonPrefab;   // Prefab for the shop buttons
    public Transform itemsParent;         // Parent transform for the buttons
    public BattleSystem battleSystem;     // Reference to the BattleSystem

    [Header("UI")]
    public TextMeshProUGUI playerCoinsText;  // Display for player coins
    public TextMeshProUGUI costDisplayText;  // TMP element to display purchase info

    private void Start()
    {
        PopulateShop();
        UpdateCoinDisplay();

        // Hide the display at start
        if (costDisplayText != null)
        {
            costDisplayText.gameObject.SetActive(false);
        }
    }

    private void PopulateShop()
    {
        // Health Upgrade
        CreateShopItem("Increase Health", 5, null, "Health", 10);

        // Attack Upgrade
        CreateShopItem("Increase Attack", 1, null, "Attack", 5);

        // Add more items here if you like...
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

    /// <summary>
    /// Updates the coin display (e.g. "Coins: 10").
    /// </summary>
    public void UpdateCoinDisplay()
    {
        if (playerCoinsText != null)
        {
            playerCoinsText.text = "Coins: " + CoinManager.Instance.totalCoins;
        }
    }

    /// <summary>
    /// Shows a message (like "Used 5 coins and added 10 Health!") for 3 seconds.
    /// </summary>
    public void ShowCostDisplay(string message)
    {
        if (costDisplayText != null)
        {
            costDisplayText.text = message;
            costDisplayText.gameObject.SetActive(true);

            // Cancel any pending hide, then re-hide after 3s
            CancelInvoke(nameof(HideCostDisplay));
            Invoke(nameof(HideCostDisplay), 3f);
        }
    }

    private void HideCostDisplay()
    {
        if (costDisplayText != null)
        {
            costDisplayText.gameObject.SetActive(false);
        }
    }
}
