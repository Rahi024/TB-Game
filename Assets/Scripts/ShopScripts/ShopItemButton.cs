using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemButton : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Upgrade Properties")]
    public int cost;
    public int upgradeValue;
    public string upgradeType;    // "Health" or "Attack"
    public int costMultiplier = 2;

    private ShopManager shopManager;
    private BattleSystem battleSystem;

    /// <summary>
    /// Called by ShopManager right after the prefab is instantiated, to set up its data.
    /// </summary>
    public void SetItem(
        string name, 
        int startingCost, 
        Sprite itemIcon, 
        string type, 
        int value, 
        ShopManager manager,
        BattleSystem battleSystem
    )
    {
        // Basic UI setup
        if (nameText)  nameText.text = name;
        if (costText)  costText.text = $"Cost: {startingCost}";
        if (iconImage) iconImage.sprite = itemIcon;

        // Store references for later
        cost          = startingCost;
        upgradeType   = type;
        upgradeValue  = value;
        shopManager   = manager;
        this.battleSystem = battleSystem;
    }

    /// <summary>
    /// Called when the player clicks the "Purchase" button.
    /// </summary>
    public void OnPurchaseButtonClicked()
    {
        if (CoinManager.Instance.totalCoins >= cost)
        {
            // Deduct coins and apply the upgrade
            CoinManager.Instance.RemoveCoins(cost);
            ApplyUpgrade();

            // Update the coin display in the UI
            shopManager.UpdateCoinDisplay();

            // Increase cost for next time
            cost *= costMultiplier;
            if (costText)
                costText.text = cost.ToString();
        }
        else
        {
            shopManager.ShowCostDisplay($"Not enough coins to buy {nameText.text}!");
        }
    }

    /// <summary>
    /// Applies the upgrade and shows the "Used X coins, added Y" message on screen.
    /// </summary>
    private void ApplyUpgrade()
    {
        switch (upgradeType)
        {
            case "Health":
                GlobalUpgrades.Instance.healthBonus += upgradeValue;
                break;
            case "Attack":
                GlobalUpgrades.Instance.attackBonus += upgradeValue;
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                return;
        }

        // Instead of Debug.Log, show on-screen message
        shopManager.ShowCostDisplay($"Used {cost} coins and added {upgradeValue} {upgradeType}!");
    }
}
