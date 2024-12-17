using System.Collections; 
using UnityEngine;

public class Bot : MonoBehaviour
{
    // Public health and damage values for the bot's attacks
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
    public void Attack(Player player)
    {
        player.TakeDamage(AttackDamage); 
    }

    // Second attack 
    public void SecondAttack(Player player)
    {
        player.TakeDamage(SecondAttackDamage); 
    }

    // Third attack method 
    public void ThirdAttack(Player player)
    {
        player.TakeDamage(ThirdAttackDamage);
    }

    // Fourth attack method 
    public void FourthAttack(Player player)
    {
        player.TakeDamage(FourthAttackDamage); 
    }

    // Charge attack method, which deals the most powerful damage to the player
    public void ChargeAttack(Player player)
    {
        player.TakeDamage(ChargeAttackDamage); 
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
        if (Health > 100) Health = 100; 
        Debug.Log($"Bot heals for {healAmount}. Current Health: {Health}");
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

    public void ApplyDotEffects()
    {
        int totalDamage = 0;

        if (burnDuration > 0)
        {
            totalDamage += burnDamagePerTurn;
            burnDuration--;
            if (burnDuration == 0)
            {
                burnDamagePerTurn = 0;
            }
        }

        if (poisonDuration > 0)
        {
            totalDamage += poisonDamagePerTurn;
            poisonDuration--;
            if (poisonDuration == 0)
            {
                poisonDamagePerTurn = 0;
            }
        }

        if (totalDamage > 0)
        {
            TakeDamage(totalDamage);
            Debug.Log($"Bot takes {totalDamage} DOT damage. Current Health: {Health}");
        }
    }
}