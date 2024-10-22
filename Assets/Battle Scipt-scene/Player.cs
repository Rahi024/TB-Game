using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 200;
    public int maxHealth = 200; // Add this to define the maximum health
    public int AttackDamage = 20;
    public int SecondAttackDamage = 25;
    public int ThirdAttackDamage = 30;
    public int FourthAttackDamage = 35;
    public int ChargeAttackDamage = 45;  

    public bool attack2Used = false;
    public bool attack3Used = false;
    public bool attack4Used = false;

    public void Attack(Bot bot)
    {
        bot.TakeDamage(AttackDamage);
    }

    public void SecondAttack(Bot bot)
    {
        bot.TakeDamage(SecondAttackDamage);
    }

    public void ThirdAttack(Bot bot)
    {
        bot.TakeDamage(ThirdAttackDamage);
    }

    public void FourthAttack(Bot bot)
    {
        bot.TakeDamage(FourthAttackDamage);
    }

    public void ChargeAttack(Bot bot)
    {
        bot.TakeDamage(ChargeAttackDamage);  
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"Player takes {damage} damage. Current Health: {Health}");
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;
        if (Health > maxHealth) Health = maxHealth; // Ensure health does not exceed maxHealth
        Debug.Log($"Player heals for {healAmount}. Current Health: {Health}");
    }
}
