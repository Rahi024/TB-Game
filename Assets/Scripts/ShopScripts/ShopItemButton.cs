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

    // Reference to ShopManager if needed
    private ShopManager shopManager;
    private BattleSystem battleSystem;

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
        // Basic setup
        if (nameText) nameText.text = name;
        if (costText) costText.text = $"Cost: {startingCost}";
        if (iconImage) iconImage.sprite = itemIcon;

        cost = startingCost;
        upgradeType = type;
        upgradeValue = value;
        shopManager = manager;
    }

    public void OnPurchaseButtonClicked()
    {
        if (CoinManager.Instance.totalCoins >= cost)
        {
            CoinManager.Instance.RemoveCoins(cost);
            ApplyUpgrade();  // <-- Writes to GlobalUpgrades
            shopManager.UpdateCoinDisplay();

            // Increase cost for next purchase
            cost *= costMultiplier;
            if (costText)
                costText.text = $"Cost: {cost}";
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private void ApplyUpgrade()
    {
        // Instead of referencing the battle scene, we update the global bonuses
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
                break;
        }

        Debug.Log($"Applied {upgradeType} upgrade of {upgradeValue}. " + 
                  $"Now: HealthBonus={GlobalUpgrades.Instance.healthBonus}, " +
                  $"AttackBonus={GlobalUpgrades.Instance.attackBonus}");
    }
}
