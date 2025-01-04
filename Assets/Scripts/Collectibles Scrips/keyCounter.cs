using UnityEngine;
using TMPro;

public class keyCounter : MonoBehaviour
{
    // Current number of keys collected in this scene
    public int keyCount;

    // Reference to the TextMeshPro fields for UI
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI coinCountText;

    // We'll store how many total keys are required for the current difficulty
    private int totalKeysNeeded;

    void Start()
    {
        // Fetch how many keys are needed based on the difficulty
        // Assumes you have a DifficultyManager with a static GetKeysNeeded() method
        totalKeysNeeded = DifficultyManager.GetKeysNeeded();

        UpdateKeyCountDisplay();
        UpdateCoinCountDisplay();
    }

    // Increments key count and awards random coins
    public void IncrementKeyCount()
    {
        keyCount++;
        Debug.Log("Key count incremented to: " + keyCount);

        // Award random coins (example: between 1 and 100)
        int randomCoins = Random.Range(1, 101);
        Debug.Log("Coins incremented by " + randomCoins);

        // If you have a CoinManager singleton, add coins
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoins(randomCoins);
        }
        else
        {
            Debug.LogError("CoinManager.Instance is null!");
        }

        // Update UI
        UpdateKeyCountDisplay();
        UpdateCoinCountDisplay();
    }

    // Updates the text to show "Keys: X / Y"
    private void UpdateKeyCountDisplay()
    {
        if (keyCountText != null)
        {
            keyCountText.text = $"Keys: {keyCount} / {totalKeysNeeded}";
        }
    }

    // If you also want to show total coins from a CoinManager in this scene
    private void UpdateCoinCountDisplay()
    {
        if (coinCountText != null && CoinManager.Instance != null)
        {
            coinCountText.text = "Coins: " + CoinManager.Instance.totalCoins;
        }
    }

    // Helper if other scripts need to know how many total keys are required
    public int GetTotalKeysNeeded()
    {
        return totalKeysNeeded;
    }
}
