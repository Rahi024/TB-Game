using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;            // Singleton reference
    public int totalCoins = 0;                     // Single source of truth for coins

    [Header("UI Reference (Optional)")]
    public TextMeshProUGUI coinText;               // If you want to display coins in the HUD

    private void Awake()
    {
        // Standard Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);         // Persist across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load coins from PlayerPrefs at the start of the game
        LoadCoins();

        // Make sure the UI text is correct at the start of the game
        UpdateCoinText();
    }

    /// <summary>
    /// Add coins and immediately update the UI text (if assigned).
    /// </summary>
    /// <param name="amount">Amount of coins to add.</param>
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        SaveCoins(); // Save the updated coin count
        UpdateCoinText();
    }

    /// <summary>
    /// Remove coins and immediately update the UI text (if assigned).
    /// </summary>
    /// <param name="amount">Amount of coins to remove.</param>
    public void RemoveCoins(int amount)
    {
        totalCoins = Mathf.Max(0, totalCoins - amount);
        SaveCoins(); // Save the updated coin count
        UpdateCoinText();
    }

    /// <summary>
    /// Updates the coinText UI to match our totalCoins.
    /// If coinText is unassigned, logs a warning (safe to ignore if you handle UI elsewhere).
    /// </summary>
    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = totalCoins.ToString();
        }
        else
        {
            Debug.LogWarning("[CoinManager] coinText reference is not assigned in the Inspector.");
        }
    }

    /// <summary>
    /// Allows a new scene or script to assign a different TMP reference at runtime.
    /// </summary>
    /// <param name="newCoinText">The new TextMeshProUGUI element to display totalCoins.</param>
    public void SetCoinTextReference(TextMeshProUGUI newCoinText)
    {
        coinText = newCoinText;
        UpdateCoinText();
    }

    /// <summary>
    /// Saves the current totalCoins value to PlayerPrefs for persistence.
    /// </summary>
    public void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the totalCoins value from PlayerPrefs.
    /// If no value exists, initializes to 0.
    /// </summary>
    public void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }
}
