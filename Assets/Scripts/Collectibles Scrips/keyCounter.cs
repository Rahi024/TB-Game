using UnityEngine;
using TMPro;

public class keyCounter : MonoBehaviour
{
    public int keyCount;                         // Reset each scene
    public TextMeshProUGUI keyCountText;         // "Keys: X"
    public TextMeshProUGUI coinCountText;        // If you want "Coins: Y" here too

    void Start()
    {
        UpdateKeyCountDisplay();
        UpdateCoinCountDisplay();
    }

    // Called from your trigger or other scripts
    public void IncrementKeyCount()
    {
        // Increase local key count
        keyCount++;
        Debug.Log("Key count incremented to: " + keyCount);

        // Award random coins (1-100) to the global manager
        int randomCoins = Random.Range(1, 101);
        Debug.Log("Coins incremented by " + randomCoins);

        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoins(randomCoins);
        }
        else
        {
            Debug.LogError("CoinManager.Instance is null!");
        }

        UpdateKeyCountDisplay();
        UpdateCoinCountDisplay();
    }

    // Refresh local key UI
    void UpdateKeyCountDisplay()
    {
        if (keyCountText != null)
        {
            keyCountText.text = "Keys: " + keyCount;
        }
    }

    // If you also want to show coins in this scene
    void UpdateCoinCountDisplay()
    {
        if (coinCountText != null && CoinManager.Instance != null)
        {
            coinCountText.text = "Coins: " + CoinManager.Instance.totalCoins;
        }
    }
}
