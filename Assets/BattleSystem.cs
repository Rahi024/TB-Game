using System.Collections;
using UnityEngine;
using UnityEngine.UI; // If using Text
using TMPro;          // If using TextMeshProUGUI

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
    private bool botHasHealed = false;  

    // Reference to UI Text (or TextMeshProUGUI)
    public TextMeshProUGUI messageText; // Uncomment if using TextMeshPro

    void Start()
    {
        StartCoroutine(BattleLoop());
    }

    void DisplayMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    IEnumerator BattleLoop()
    {
        while (!battleOver)
        {
            if (playerTurn)
            {
                DisplayMessage("Player turn to select a move!");
                yield return new WaitUntil(() => !playerTurn);
            }
            else
            {
                DisplayMessage("Bot turn to select a move!");
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

    public IEnumerator PlayerHeal()  // Changed from void to IEnumerator
    {
        if (playerTurn && !battleOver && playerHealCounter < 2)  
        {
            player.Heal(30);  
            DisplayMessage("Player healed by 30 health.");
            yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
            DisplayMessage($"Player's Current Health: {player.Health}");
            playerHealCounter++; 
            CheckBattleOutcome();
            FindObjectOfType<BattleUI>().UpdateHealthSliders();  
            playerTurn = false;
        }
        else
        {
            DisplayMessage("Player has no more heals available!");
            yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
        }
    }

    IEnumerator PlayerTurn(int attackType)
    {
        switch (attackType)
        {
            case 1:
                DisplayMessage("Player uses Attack 1!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                player.Attack(bot);
                DisplayMessage($"Bot takes {player.AttackDamage} damage. Bot's Current Health: {bot.Health}");
                break;
            case 2:
                DisplayMessage("Player uses Attack 2!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                player.SecondAttack(bot);
                DisplayMessage($"Bot takes {player.SecondAttackDamage} damage. Bot's Current Health: {bot.Health}");
                break;
            case 3:
                DisplayMessage("Player uses Attack 3!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                player.ThirdAttack(bot);
                DisplayMessage($"Bot takes {player.ThirdAttackDamage} damage. Bot's Current Health: {bot.Health}");
                break;
            case 4:
                DisplayMessage("Player uses Attack 4!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                player.FourthAttack(bot);
                DisplayMessage($"Bot takes {player.FourthAttackDamage} damage. Bot's Current Health: {bot.Health}");
                break;
            default:
                DisplayMessage("Unknown Attack!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                break;
        }

        normalAttackCounter++;

        if (normalAttackCounter >= 3)
        {
            chargeAttackAvailable = true;
            DisplayMessage("Player's Charge attack is now available!");
            yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
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
        DisplayMessage("Player uses Charge Attack!");
        yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
        player.ChargeAttack(bot);  
        DisplayMessage($"Bot takes {player.ChargeAttackDamage} damage. Bot's Current Health: {bot.Health}");
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
            DisplayMessage("Bot healed by 30 health.");
            yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
            DisplayMessage($"Bot's Current Health: {bot.Health}");
            botHealCounter++; 
            botHasHealed = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();
        }
        else if (botChargeAttackAvailable)
        {
            if (Random.value > 0.1f)
            {
                DisplayMessage("Bot uses Charge Attack!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                bot.ChargeAttack(player);
                DisplayMessage($"Player takes {bot.ChargeAttackDamage} damage. Player's Current Health: {player.Health}");
                FindObjectOfType<BattleUI>().UpdateHealthSliders();  
                botChargeAttackAvailable = false;
                botNormalAttackCounter = 0;
            }
            else
            {
                int randomAttack = Random.Range(1, 5);  
                yield return PerformBotRandomAttack(randomAttack);  // Change to yield return
            }
        }
        else
        {
            int randomAttack = Random.Range(1, 5);  
            yield return PerformBotRandomAttack(randomAttack);  // Change to yield return
        }

        botNormalAttackCounter++;  

        if (botNormalAttackCounter >= 3)  
        {
            botChargeAttackAvailable = true;
            DisplayMessage("Bot's Charge attack is now available!");
            yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
        }

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);  
    }

    IEnumerator PerformBotRandomAttack(int attackType)  // Changed from void to IEnumerator
    {
        switch (attackType)
        {
            case 1:
                DisplayMessage("Bot uses Attack 1!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                bot.Attack(player);
                DisplayMessage($"Player takes {bot.AttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            case 2:
                DisplayMessage("Bot uses Attack 2!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                bot.SecondAttack(player);
                DisplayMessage($"Player takes {bot.SecondAttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            case 3:
                DisplayMessage("Bot uses Attack 3!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                bot.ThirdAttack(player);
                DisplayMessage($"Player takes {bot.ThirdAttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            case 4:
                DisplayMessage("Bot uses Attack 4!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                bot.FourthAttack(player);
                DisplayMessage($"Player takes {bot.FourthAttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            default:
                DisplayMessage("Unknown Bot Attack!");
                yield return new WaitForSeconds(1f);  // Add delay to allow this message to be shown
                break;
        }

        FindObjectOfType<BattleUI>().UpdateHealthSliders();
    }

    void CheckBattleOutcome()
    {
        if (player.Health <= 0)
        {
            player.Health = 0; // Ensure health is set to zero if it goes below
            DisplayMessage("Player Lost The Battle!");
            battleOver = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();  // Ensure slider is updated
        }
        else if (bot.Health <= 0)
        {
            bot.Health = 0; // Ensure health is set to zero if it goes below
            DisplayMessage("Bot Lost The Battle!");
            battleOver = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();  // Ensure slider is updated
        }
    }
}