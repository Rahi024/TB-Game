using UnityEngine;

public class GlobalUpgrades : MonoBehaviour
{
    public static GlobalUpgrades Instance;

    // These fields store the cumulative upgrades purchased in the shop
    public int healthBonus = 0;
    public int attackBonus = 0;
    // If you want defense or other stats, you can add them here.

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
}
