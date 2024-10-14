using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image playerHealthBar;
    public Image enemyHealthBar;
    public TextMeshProUGUI combatLog;

    public void UpdateHealthBar(Image healthBar, float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
        healthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);
    }

    public void LogCombatEvent(string message)
    {
        combatLog.text += message + "\n";
    }

    public void StartCombat()
    {
        LogCombatEvent("Combat has started!");
    }

    public void ShowCardOptions(Player player)
    {
        // Display card options to the player (this is a placeholder for actual UI implementation)
        LogCombatEvent("Choose a card to play.");
    }

    public void ShowActionResult(string result)
    {
        LogCombatEvent(result);
    }
}
