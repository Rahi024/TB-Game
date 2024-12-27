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
    public int Attack(Bot bot, out bool isCrit)
    {
        int baseDamage = AttackDamage;
        bool critHappened = false;

        // 30% chance to crit
        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        // Deal damage
        bot.TakeDamage(baseDamage);

        // Output parameters
        isCrit = critHappened;
        return baseDamage;  // This is the *actual final damage* dealt
    }

    // Second attack method
    public int SecondAttack(Bot bot, out bool isCrit)
    {
        int baseDamage = SecondAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        bot.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Third attack method
    public int ThirdAttack(Bot bot, out bool isCrit)
    {
        int baseDamage = ThirdAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        bot.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Fourth attack method
    public int FourthAttack(Bot bot, out bool isCrit)
    {
        int baseDamage = FourthAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        bot.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Charge attack method, which is the most powerful attack
    public int ChargeAttack(Bot bot, out bool isCrit)
    {
        int baseDamage = ChargeAttackDamage;
        bool critHappened = false;

        if (Random.value < 0.3f)
        {
            baseDamage = Mathf.RoundToInt(baseDamage * 1.25f);
            critHappened = true;
        }

        bot.TakeDamage(baseDamage);
        isCrit = critHappened;
        return baseDamage;
    }

    // Method for taking damage from the bot
    public void TakeDamage(int damage)
    {
        if (shieldActive)
        {
            damage = Mathf.RoundToInt(damage * (1 - shieldReduction));
            shieldActive = false; // shield only lasts for one hit
        }
        Health -= damage;
        Debug.Log($"Player takes {damage} damage. Current Health: {Health}");
    }

    // Method for healing the player by a specified amount
    public void Heal(int healAmount)
    {
        Health += healAmount;
        if (Health > maxHealth) Health = maxHealth;
        Debug.Log($"Player heals for {healAmount}. Current Health: {Health}");
    }
}