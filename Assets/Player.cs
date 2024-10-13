using System.Collections; 
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 100;
    public int AttackDamage = 20;
    public int SecondAttackDamage = 25;
    public int ThirdAttackDamage = 30;
    public int FourthAttackDamage = 35;
    public int ChargeAttackDamage = 50;  

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
        if (Health > 100) Health = 100;
        Debug.Log($"Player heals for {healAmount}. Current Health: {Health}");
    }

}