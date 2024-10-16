using System.Collections; 
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Bot : MonoBehaviour
{
    public int Health = 100;
    public int AttackDamage = 15;
    public int SecondAttackDamage = 20;
    public int ThirdAttackDamage = 25;
    public int FourthAttackDamage = 30;
    public int ChargeAttackDamage = 40;
    private readonly BattleUI battleUI;
    public Player player;

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
        battleUI.LogCombatEvent($"Bot takes {damage} damage. Current Health: {Health}");
        player.Exp -= damage;
    }
    public void Heal(int healAmount)
    {
        Health += healAmount;
        if (Health > 100) Health = 100; 
        battleUI.LogCombatEvent($"Bot heals for {healAmount}. Current Health: {Health}");
    }

}