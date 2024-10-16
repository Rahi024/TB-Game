using System.Collections; 
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public Player player;
    public Bot bot;
    private readonly BattleUI battleUI;

    public bool playerTurn = true;  
    private bool battleOver = false;

    // Player charge attack variables
    private int normalAttackCounter = 0; 
    public bool chargeAttackAvailable = false;  

    // Bot charge attack variables
    private int botNormalAttackCounter = 0;
    public bool botChargeAttackAvailable = false;

    // Healing variables
    public int playerHealCounter = 0;  
    public int botHealCounter = 0;     
    private bool botHasHealed = false;  // To track if bot can heal

    void Start()
    {
        StartCoroutine(BattleLoop());
    }

    public IEnumerator BattleLoop()
    {
        while (!battleOver)
        {
            if (playerTurn)
            {
                battleUI.LogCombatEvent("Player turn to select a move!");
                yield return new WaitUntil(() => !playerTurn);  
            }
            else
            {
                battleUI.LogCombatEvent("Bot turn to select a move!");
                yield return StartCoroutine(BotTurn());
                playerTurn = true;  
            }
        }
    }

    public void PlayerAttack(int attackType)
    {
        if (playerTurn && !battleOver)
        {
            StartCoroutine(PlayerTurn(attackType));  
        }
    }

    public void PlayerChargeAttack()
    {
        if (playerTurn && !battleOver && chargeAttackAvailable)
        {
            StartCoroutine(PlayerChargeAttackTurn());  
        }
    }

    public void PlayerHeal()  
    {
        if (playerTurn && !battleOver && playerHealCounter < 2)  
        {
            player.Heal(30);  
            battleUI.LogCombatEvent("Player healed by 30 health.");
            playerHealCounter++; 
            CheckBattleOutcome();
            FindFirstObjectByType<BattleUI>().UpdateHealthSliders();  
            playerTurn = false;
        }
        else
        {
            battleUI.LogCombatEvent("Player has no more heals available!");
        }
    }

    IEnumerator PlayerTurn(int attackType)
    {
        switch (attackType)
        {
            case 1:
                battleUI.LogCombatEvent("Player uses Attack 1!");
                player.Attack(bot);
                break;
            case 2:
                battleUI.LogCombatEvent("Player uses Attack 2!");
                player.SecondAttack(bot);
                break;
            case 3:
                battleUI.LogCombatEvent("Player uses Attack 3!");
                player.ThirdAttack(bot);
                break;
            case 4:
                battleUI.LogCombatEvent("Player uses Attack 4!");
                player.FourthAttack(bot);
                break;
            default:
                battleUI.LogCombatEvent("Unknown Attack!");
                break;
        }

        normalAttackCounter++;  

        if (normalAttackCounter >= 3)  
        {
            chargeAttackAvailable = true;
            battleUI.LogCombatEvent("Player's Charge attack is now available!");
        }

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);  

        if (!battleOver)
        {
            playerTurn = false;  
        }
    }

    IEnumerator PlayerChargeAttackTurn()
    {
        battleUI.LogCombatEvent("Player uses Charge Attack!");
        player.ChargeAttack(bot);  
        chargeAttackAvailable = false;  
        normalAttackCounter = 0;  

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);  

        if (!battleOver)
        {
            playerTurn = false;  
        }
    }

    IEnumerator BotTurn()
    {
        if (botHealCounter < 2 && bot.Health <= 30 && !botHasHealed)  
        {
            bot.Heal(30);  
            battleUI.LogCombatEvent("Bot healed by 30 health.");
            botHealCounter++; 
            botHasHealed = true;

            FindFirstObjectByType<BattleUI>().UpdateHealthSliders();
        }
        else if (botChargeAttackAvailable)
        {
            if (Random.value > 0.1f)  // 90% chance to use charge attack
            {
                battleUI.LogCombatEvent("Bot uses Charge Attack!");
                bot.ChargeAttack(player);
                FindFirstObjectByType<BattleUI>().UpdateHealthSliders();  
                botChargeAttackAvailable = false;
                botNormalAttackCounter = 0;
            }
            else
            {
                int randomAttack = Random.Range(1, 5);  
                PerformBotRandomAttack(randomAttack);
            }
        }
        else
        {
            int randomAttack = Random.Range(1, 5);  
            PerformBotRandomAttack(randomAttack);
        }

        botNormalAttackCounter++;  

        if (botNormalAttackCounter >= 3)  
        {
            botChargeAttackAvailable = true;
            battleUI.LogCombatEvent("Bot's Charge attack is now available!");
        }

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);  
    }

    void PerformBotRandomAttack(int attackType)
    {
        switch (attackType)
        {
            case 1:
                battleUI.LogCombatEvent("Bot uses Attack 1!");
                bot.Attack(player);
                break;
            case 2:
                battleUI.LogCombatEvent("Bot uses Attack 2!");
                bot.SecondAttack(player);
                break;
            case 3:
                battleUI.LogCombatEvent("Bot uses Attack 3!");
                bot.ThirdAttack(player);
                break;
            case 4:
                battleUI.LogCombatEvent("Bot uses Attack 4!");
                bot.FourthAttack(player);
                break;
            default:
                battleUI.LogCombatEvent("Unknown Bot Attack!");
                break;
        }

        FindFirstObjectByType<BattleUI>().UpdateHealthSliders();  
    }

    void CheckBattleOutcome()
    {
        if (player.Health <= 0)
        {
            battleUI.LogCombatEvent("Player Lost The Battle!");
            battleOver = true;
        }
        else if (bot.Health <= 0)
        {
            battleUI.LogCombatEvent("Bot Lost The Battle!");
            battleOver = true;
            player.MaxExp *= 2; 
        }
    }
}
