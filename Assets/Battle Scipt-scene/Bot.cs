using System.Collections; 
using UnityEngine;

public class Bot : MonoBehaviour
{
    public int Health = 200;
    public int AttackDamage = 20;
    public int SecondAttackDamage = 30;
    public int ThirdAttackDamage = 35;
    public int FourthAttackDamage = 40;
    public int ChargeAttackDamage = 45;  

    public void Attack(Player player)
    {
        player.TakeDamage(AttackDamage);
    }

    public void SecondAttack(Player player)
    {
        player.TakeDamage(SecondAttackDamage);
    }

    public void ThirdAttack(Player player)
    {
        player.TakeDamage(ThirdAttackDamage);
    }

    public void FourthAttack(Player player)
    {
        player.TakeDamage(FourthAttackDamage);
    }

    public void ChargeAttack(Player player)
    {
        player.TakeDamage(ChargeAttackDamage);  
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"Bot takes {damage} damage. Current Health: {Health}");
    }
    public void Heal(int healAmount)
    {
        Health += healAmount;
        if (Health > 100) Health = 100; 
        Debug.Log($"Bot heals for {healAmount}. Current Health: {Health}");
    }

}