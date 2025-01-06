using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Setup")]
    public GameObject itemButtonPrefab;  // Prefab for the shop buttons
    public Transform itemsParent;        // Parent transform for the buttons
    public BattleSystem battleSystem;    // Reference to the BattleSystem

    [Header("UI")]
    public TextMeshProUGUI playerCoinsText;  // Display for total player coins

    // TextMeshPro fields to display the NEXT cost for Health & Attack
    public TextMeshProUGUI healthCostText;
    public TextMeshProUGUI attackCostText;

    // Feedback UI for "Used X coins" message
    public TextMeshProUGUI costDisplayText;

    private ShopItemButton healthButton; // Reference to the health button instance
    private ShopItemButton attackButton; // Reference to the attack button instance

    private void Start()
    {
        PopulateShop();
        UpdateCoinDisplay();

        // Hide the "Used X coins" feedback at start
        if (costDisplayText != null)
        {
            costDisplayText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Creates the shop items and sets up their next sale costs.
    /// </summary>
    private void PopulateShop()
    {
        // Create Health and Attack items
        healthButton = CreateShopItem("Increase Health", 5, null, "Health", 10);
        attackButton = CreateShopItem("Increase Attack", 1, null, "Attack", 5);

        // Display the NEXT costs for the items in the UI
        UpdateNextSaleCosts();
    }

    private ShopItemButton CreateShopItem(string name, int startingCost, Sprite icon, string type, int value)
    {
        // Instantiate the button prefab
        GameObject newButton = Instantiate(itemButtonPrefab, itemsParent);

        // Get the ShopItemButton script from the prefab
        ShopItemButton buttonScript = newButton.GetComponent<ShopItemButton>();
        if (buttonScript != null)
        {
            // Pass all the required data
            buttonScript.SetItem(name, startingCost, icon, type, value, this, battleSystem);
        }

        // Return the button script reference
        return buttonScript;
    }

    /// <summary>
    /// Updates the coin display and recalculates the next sale costs.
    /// </summary>
   public void UpdateCoinDisplay()
{
    if (playerCoinsText != null)
    {
        playerCoinsText.text = CoinManager.Instance.totalCoins.ToString();
    }

    // Immediately update the displayed next sale costs
    UpdateNextSaleCosts();
}

public void UpdateNextSaleCosts()
{
    if (healthCostText != null && healthButton != null)
    {
        // Always fetch the most up-to-date cost for the next purchase
        int nextHealthCost = healthButton.cost * healthButton.costMultiplier;
        healthCostText.text = $"Next Cost: {nextHealthCost}";
    }

    if (attackCostText != null && attackButton != null)
    {
        // Always fetch the most up-to-date cost for the next purchase
        int nextAttackCost = attackButton.cost * attackButton.costMultiplier;
        attackCostText.text = $"Next Cost: {nextAttackCost}";
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
