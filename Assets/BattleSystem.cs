using System.Collections; 
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public Player player;
    public Bot bot;

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

    IEnumerator BattleLoop()
    {
        while (!battleOver)
        {
            if (playerTurn)
            {
                Debug.Log("Player turn to select a move!");
                yield return new WaitUntil(() => !playerTurn);  
            }
            else
            {
                Debug.Log("Bot turn to select a move!");
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
            Debug.Log("Player healed by 30 health.");
            playerHealCounter++; 
            CheckBattleOutcome();
            FindObjectOfType<BattleUI>().UpdateHealthSliders();  
            playerTurn = false;
        }
        else
        {
            Debug.Log("Player has no more heals available!");
        }
    }

    IEnumerator PlayerTurn(int attackType)
    {
        switch (attackType)
        {
            case 1:
                Debug.Log("Player uses Attack 1!");
                player.Attack(bot);
                break;
            case 2:
                Debug.Log("Player uses Attack 2!");
                player.SecondAttack(bot);
                break;
            case 3:
                Debug.Log("Player uses Attack 3!");
                player.ThirdAttack(bot);
                break;
            case 4:
                Debug.Log("Player uses Attack 4!");
                player.FourthAttack(bot);
                break;
            default:
                Debug.Log("Unknown Attack!");
                break;
        }

        normalAttackCounter++;  

        if (normalAttackCounter >= 3)  
        {
            chargeAttackAvailable = true;
            Debug.Log("Player's Charge attack is now available!");
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
        Debug.Log("Player uses Charge Attack!");
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
            Debug.Log("Bot healed by 30 health.");
            botHealCounter++; 
            botHasHealed = true;

            FindObjectOfType<BattleUI>().UpdateHealthSliders();
        }
        else if (botChargeAttackAvailable)
        {
            if (Random.value > 0.1f)  // 90% chance to use charge attack
            {
                Debug.Log("Bot uses Charge Attack!");
                bot.ChargeAttack(player);
                FindObjectOfType<BattleUI>().UpdateHealthSliders();  
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
            Debug.Log("Bot's Charge attack is now available!");
        }

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);  
    }

    void PerformBotRandomAttack(int attackType)
    {
        switch (attackType)
        {
            case 1:
                Debug.Log("Bot uses Attack 1!");
                bot.Attack(player);
                break;
            case 2:
                Debug.Log("Bot uses Attack 2!");
                bot.SecondAttack(player);
                break;
            case 3:
                Debug.Log("Bot uses Attack 3!");
                bot.ThirdAttack(player);
                break;
            case 4:
                Debug.Log("Bot uses Attack 4!");
                bot.FourthAttack(player);
                break;
            default:
                Debug.Log("Unknown Bot Attack!");
                break;
        }

        FindObjectOfType<BattleUI>().UpdateHealthSliders();  
    }

    void CheckBattleOutcome()
    {
        if (player.Health <= 0)
        {
            Debug.Log("Player Lost The Battle!");
            battleOver = true;
        }
        else if (bot.Health <= 0)
        {
            Debug.Log("Bot Lost The Battle!");
            battleOver = true;
        }
    }
}
