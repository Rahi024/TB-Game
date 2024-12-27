using UnityEngine;

public class GlobalUpgrades : MonoBehaviour
{
    public static GlobalUpgrades Instance;

    // These fields store the cumulative upgrades purchased in the shop
    public int healthBonus = 0;
    public int attackBonus = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Survive scene loads
        }
        else
        {
            Destroy(gameObject); // Only one instance allowed
        }
    }

    /// <summary>
    /// Resets all global upgrades to their default values.
    /// </summary>
    public void ResetUpgrades()
    {
        healthBonus = 0;
        attackBonus = 0;

        // Optionally clear persistent data if upgrades are saved
        PlayerPrefs.DeleteKey("HealthBonus");
        PlayerPrefs.DeleteKey("AttackBonus");
    }
}
