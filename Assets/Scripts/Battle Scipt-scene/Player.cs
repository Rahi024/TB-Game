using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Public fields for player health and damage values
    public int Health = 200;  
    public int maxHealth = 200;  
    public int AttackDamage = 20;  
    public int SecondAttackDamage = 25; 
    public int ThirdAttackDamage = 30;  
    public int FourthAttackDamage = 35; 
    public int ChargeAttackDamage = 45;  

    // Flags to track if certain attacks have been used (Attack 2, 3, and 4)
    public bool attack2Used = false;  
    public bool attack3Used = false;  
    public bool attack4Used = false;  

    // Shield status
    public bool shieldActive = false;
    public float shieldReduction = 0.5f; // 50% damage reduction

    // Standard attack method that deals damage to the bot
    public void Attack(Bot bot)
    {
        bot.TakeDamage(AttackDamage);  
    }

    // Second attack method
    public void SecondAttack(Bot bot)
    {
        bot.TakeDamage(SecondAttackDamage);  
    }

    // Third attack method
    public void ThirdAttack(Bot bot)
    {
        bot.TakeDamage(ThirdAttackDamage);  
    }

    // Fourth attack method
    public void FourthAttack(Bot bot)
    {
        bot.TakeDamage(FourthAttackDamage);  
    }

    // Charge attack method, which is the most powerful attack
    public void ChargeAttack(Bot bot)
    {
        bot.TakeDamage(ChargeAttackDamage);  
    }

    // Method for taking damage from the bot
    public void TakeDamage(int damage)
    {
        if (shieldActive)
        {
            damage = Mathf.RoundToInt(damage * (1 - shieldReduction));
            shieldActive = false; // shield lasts only one turn
        }
        // Reduce player's health by the damage value
        Health -= damage;
        Debug.Log($"Player takes {damage} damage. Current Health: {Health}");
    }

    // Method for healing the player by a specified amount
    public void Heal(int healAmount)
    {
        // Increase player's health by the heal amount
        Health += healAmount;
        if (Health > maxHealth) Health = maxHealth;
        Debug.Log($"Player heals for {healAmount}. Current Health: {Health}");
    }
}