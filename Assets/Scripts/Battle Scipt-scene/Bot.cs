using System.Collections; 
using UnityEngine;

public class Bot : MonoBehaviour
{
    // Public health and damage values
    public int Health = 200; 
    public int AttackDamage = 20; 
    public int SecondAttackDamage = 30; 
    public int ThirdAttackDamage = 35; 
    public int FourthAttackDamage = 40; 
    public int ChargeAttackDamage = 45; 

    // Status effect fields
    public int burnDamagePerTurn = 0;
    public int burnDuration = 0;
    public int poisonDamagePerTurn = 0;
    public int poisonDuration = 0;

    // Basic attack method that reduces the player's health 
    public int Attack(Player player, out bool isCrit)
    {
        int baseDamage = AttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        player.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Second attack 
    public int SecondAttack(Player player, out bool isCrit)
    {
        int baseDamage = SecondAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        player.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Third attack method 
    public int ThirdAttack(Player player, out bool isCrit)
    {
        int baseDamage = ThirdAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        player.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Fourth attack method 
    public int FourthAttack(Player player, out bool isCrit)
    {
        int baseDamage = FourthAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        player.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Charge attack method, which deals the most powerful damage to the player
    public int ChargeAttack(Player player, out bool isCrit)
    {
        int baseDamage = ChargeAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        player.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Method to take damage when the player attacks the bot
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"Bot takes {damage} damage. Current Health: {Health}");
    }

    // Method to heal the bot by a specified amount
    public void Heal(int healAmount)
    {
        // Increase bot's health by the heal amount
        Health += healAmount;
        // If your intended max for the bot is 100, keep this
        if (Health > 100) Health = 100; 
        Debug.Log($"Bot heals for {healAmount}. Current Health: {Health}");
    }

    public int ApplyDotEffects()
    {
        int totalDamage = 0;

        // Burn
        if (burnDuration > 0)
        {
            totalDamage += burnDamagePerTurn;
            burnDuration--;
            if (burnDuration == 0)
            {
                burnDamagePerTurn = 0;
            }
        }

        // Poison
        if (poisonDuration > 0)
        {
            totalDamage += poisonDamagePerTurn;
            poisonDuration--;
            if (poisonDuration == 0)
            {
                poisonDamagePerTurn = 0;
            }
        }

        // Apply the total DOT if > 0
        if (totalDamage > 0)
        {
            TakeDamage(totalDamage);
            Debug.Log($"Bot takes {totalDamage} DOT damage. Current Health: {Health}");
        }

        return totalDamage;
    }

    public void ApplyBurn(int damage, int duration)
    {
        burnDamagePerTurn = damage;
        burnDuration = duration;
    }

    public void ApplyPoison(int damage, int duration)
    {
        poisonDamagePerTurn = damage;
        poisonDuration = duration;
    }
}