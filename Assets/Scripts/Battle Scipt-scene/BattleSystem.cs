using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    // Public references to Player and Bot objects
    public Player player;
    public Bot bot;

    public bool playerTurn = true;  
    private bool battleOver = false;

    // Counters for normal and charge attacks
    private int normalAttackCounter = 0; 
    public bool chargeAttackAvailable = false;  

    private int botNormalAttackCounter = 0;
    public bool botChargeAttackAvailable = false;

    // Heal counters for both player and bot 
    public int playerHealCounter = 0;  
    public int botHealCounter = 0;     
    private bool botHasHealed = false;  // Track if bot has healed during the game

    public TextMeshProUGUI messageText; 

    public GameOverScript gameOverScript;

    void Start()
    {
        StartCoroutine(BattleLoop());
    }

    void Update () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void DisplayMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;  
        }
    }

    // Alternating turns between player and bot
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
                playerTurn = true;  // After bot finishes, give control back to the player
            }
        }
    }

    // Called when the player selects an attack
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
            // Start the charge attack turn if it's available
            StartCoroutine(PlayerChargeAttackTurn());
        }
    }

    // Heal action for the player
    public IEnumerator PlayerHeal()
    {
        // Player can heal if below max health and hasn't healed more than twice
        if (playerTurn && !battleOver && playerHealCounter < 2 && player.Health < player.maxHealth)
        {
            player.Heal(30);  // Heal player by 30 health points
            DisplayMessage("Player healed by 30 health.");
            yield return new WaitForSeconds(1f);  
            DisplayMessage($"Player's Current Health: {player.Health}");
            playerHealCounter++;  // Increment the heal counter
            CheckBattleOutcome();  
            FindObjectOfType<BattleUI>().UpdateHealthSliders();  // Update the UI health sliders
            playerTurn = false;  // End player's turn
        }
        else if (player.Health >= player.maxHealth)
        {
            DisplayMessage("Player is already at full health!");  
            yield return new WaitForSeconds(1f);
        }
        else
        {
            DisplayMessage("Player has no more heals available!");  // No healing if out of heals
            yield return new WaitForSeconds(1f);  
        }
    }

    // Handle player's turn based on selected attack type
    IEnumerator PlayerTurn(int attackType)
    {
        switch (attackType)
        {
            case 1:
                DisplayMessage("Player uses Attack 1!");
                yield return new WaitForSeconds(1f);
                player.Attack(bot);
                DisplayMessage($"Bot takes {player.AttackDamage} damage. Bot's Current Health: {bot.Health}");
                break;
            case 2:
                if (!player.attack2Used)
                {
                    DisplayMessage("Player uses Attack 2!");
                    yield return new WaitForSeconds(1f);
                    player.SecondAttack(bot);
                    DisplayMessage($"Bot takes {player.SecondAttackDamage} damage. Bot's Current Health: {bot.Health}");
                    player.attack2Used = true;  // Mark the attack as used
                }
                else
                {
                    DisplayMessage("Attack 2 is not available.");
                }
                break;
            case 3:
                if (!player.attack3Used)
                {
                    DisplayMessage("Player uses Attack 3!");
                    yield return new WaitForSeconds(1f);
                    player.ThirdAttack(bot);
                    DisplayMessage($"Bot takes {player.ThirdAttackDamage} damage. Bot's Current Health: {bot.Health}");
                    player.attack3Used = true;  
                }
                else
                {
                    DisplayMessage("Attack 3 is not available.");
                }
                break;
            case 4:
                if (!player.attack4Used)
                {
                    DisplayMessage("Player uses Attack 4!");
                    yield return new WaitForSeconds(1f);
                    player.FourthAttack(bot);
                    DisplayMessage($"Bot takes {player.FourthAttackDamage} damage. Bot's Current Health: {bot.Health}");
                    player.attack4Used = true;  // Mark the attack as used
                }
                else
                {
                    DisplayMessage("Attack 4 is not available.");
                }
                break;
            default:
                DisplayMessage("Unknown Attack!");  // Handle invalid attack selection
                yield return new WaitForSeconds(1f);
                break;
        }

        // 70% chance to reset the attack after use
        float resetChance = Random.value;  // Generates a random value between 0 and 1
        if (resetChance <= 0.7f)
        {
            // Restore used attack based on attack type
            if (attackType == 2)
            {
                player.attack2Used = false;
                DisplayMessage("Attack 2 has been restored!");
            }
            else if (attackType == 3)
            {
                player.attack3Used = false;
                DisplayMessage("Attack 3 has been restored!");
            }
            else if (attackType == 4)
            {
                player.attack4Used = false;
                DisplayMessage("Attack 4 has been restored!");
            }
        }

        // Track number of normal attacks to make charge attack available
        normalAttackCounter++;

        // If the player has attacked 3 times, the charge attack becomes available
        if (normalAttackCounter >= 3)
        {
            chargeAttackAvailable = true;
            DisplayMessage("Player's Charge attack is now available!");
            yield return new WaitForSeconds(1f);
        }

        CheckBattleOutcome();  // Check if the battle has ended
        yield return new WaitForSeconds(1f);
        if (!battleOver)
        {
            playerTurn = false;
        }
    }

    // Handle player's charge attack
    IEnumerator PlayerChargeAttackTurn()
    {
        DisplayMessage("Player uses Charge Attack!");
        yield return new WaitForSeconds(1f);
        player.ChargeAttack(bot);  // Perform the charge attack
        DisplayMessage($"Bot takes {player.ChargeAttackDamage} damage. Bot's Current Health: {bot.Health}");
        chargeAttackAvailable = false;  // Reset charge attack availability
        normalAttackCounter = 0;  // Reset normal attack counter

        CheckBattleOutcome();  // Check if battle is over
        yield return new WaitForSeconds(1f);

        if (!battleOver)
        {
            playerTurn = false;  
        }
    }

    IEnumerator BotTurn()
    {
        // Bot heals if it's low on health and hasn't healed more than twice
        if (botHealCounter < 2 && bot.Health <= 30 && !botHasHealed)
        {
            bot.Heal(30);  // Heal bot by 30 health points
            DisplayMessage("Bot healed by 30 health.");
            yield return new WaitForSeconds(1f);
            DisplayMessage($"Bot's Current Health: {bot.Health}");
            botHealCounter++;  
            botHasHealed = true;  
            FindObjectOfType<BattleUI>().UpdateHealthSliders();  // Update health sliders in UI
        }
        else if (botChargeAttackAvailable)
        {
            // 90% chance for bot to use charge attack when available
            if (Random.value > 0.1f)
            {
                DisplayMessage("Bot uses Charge Attack!");
                yield return new WaitForSeconds(1f);
                bot.ChargeAttack(player);  // Perform charge attack on player
                DisplayMessage($"Player takes {bot.ChargeAttackDamage} damage. Player's Current Health: {player.Health}");
                FindObjectOfType<BattleUI>().UpdateHealthSliders();  // Update health sliders
                botChargeAttackAvailable = false;  // Reset charge attack
                botNormalAttackCounter = 0;  // Reset bot's normal attack counter
            }
            else
            {
                // Bot randomly picks another attack
                int randomAttack = Random.Range(1, 5);
                yield return PerformBotRandomAttack(randomAttack);
            }
        }
        else
        {
            // Bot randomly picks another attack
            int randomAttack = Random.Range(1, 5);
            yield return PerformBotRandomAttack(randomAttack);
        }

        // Track bot's normal attack usage
        botNormalAttackCounter++;

        // Bot's charge attack becomes available after 3 normal attacks
        if (botNormalAttackCounter >= 3)
        {
            botChargeAttackAvailable = true;
            DisplayMessage("Bot's Charge attack is now available!");
            yield return new WaitForSeconds(1f);
        }

        CheckBattleOutcome();  
        yield return new WaitForSeconds(1f);
    }

    // Perform a random attack by the bot based on the selected attack type
    IEnumerator PerformBotRandomAttack(int attackType)
    {
        switch (attackType)
        {
            case 1:
                DisplayMessage("Bot uses Attack 1!");
                yield return new WaitForSeconds(1f);
                bot.Attack(player);  // Perform basic attack
                DisplayMessage($"Player takes {bot.AttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            case 2:
                DisplayMessage("Bot uses Attack 2!");
                yield return new WaitForSeconds(1f);
                bot.SecondAttack(player);  // Perform second attack
                DisplayMessage($"Player takes {bot.SecondAttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            case 3:
                DisplayMessage("Bot uses Attack 3!");
                yield return new WaitForSeconds(1f);
                bot.ThirdAttack(player);  // Perform third attack
                DisplayMessage($"Player takes {bot.ThirdAttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            case 4:
                DisplayMessage("Bot uses Attack 4!");
                yield return new WaitForSeconds(1f);
                bot.FourthAttack(player);  // Perform fourth attack
                DisplayMessage($"Player takes {bot.FourthAttackDamage} damage. Player's Current Health: {player.Health}");
                break;
            default:
                DisplayMessage("Unknown Bot Attack!");  // Handle invalid attack selection
                yield return new WaitForSeconds(1f);
                break;
        }

        // Update health sliders in the UI
        FindObjectOfType<BattleUI>().UpdateHealthSliders();
    }

    // Check the battle outcome to determine if either the player or bot has won
    void CheckBattleOutcome()
    {
        if (player.Health <= 0)
        {
            // Player has lost
            player.Health = 0;
            DisplayMessage("Player Lost The Battle!");
            battleOver = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();
            gameOverScript.gameOver();  // Reload the first scene
        }
        else if (bot.Health <= 0)
        {
            // Bot has lost
            bot.Health = 0;
            DisplayMessage("Bot Lost The Battle!");
            battleOver = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();
            SceneManager.LoadScene("scene2");  // Load the victory scene
            //gameOverScript.gameOver(); // Load the victory scene
        }
    }
}