using System.Collections; 
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health;
    public int MaxHealth = 100;
    public int Exp;
    public int MaxExp = 100;
    public int AttackDamage = 20;
    public int SecondAttackDamage = 25;
    public int ThirdAttackDamage = 30;
    public int FourthAttackDamage = 35;
    public int ChargeAttackDamage = 50;
    private readonly BattleSystem battleSystem;
    private readonly BattleUI battleUI;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arena"))
        {
            StartCoroutine(battleSystem.BattleLoop());
        }
    }

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
        battleUI.LogCombatEvent($"Player takes {damage} damage. Current Health: {Health}");
    }
    public void Heal(int healAmount)
    {
        Health += healAmount;
        if (Health > 100) Health = 100;
        battleUI.LogCombatEvent($"Player heals for {healAmount}. Current Health: {Health}");
    }


    void Start()
    {
        Health = MaxHealth;
        Exp = MaxExp;

    }

}