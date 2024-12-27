using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    private bool botHasHealed = false; // Track if bot has healed during the game

    public TextMeshProUGUI messageText;
    public GameOverScript gameOverScript;
    private bool isPlayerBusy = false;

    void Awake()
    {
        // Apply any purchased upgrades before starting the game
        ApplyGlobalUpgradesToPlayer();
    }

    void Start()
    {
        StartCoroutine(BattleLoop());
    }

    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ApplyGlobalUpgradesToPlayer()
    {
        // Apply upgrades from a global upgrades system
        if (player != null)
        {
            player.maxHealth += GlobalUpgrades.Instance.healthBonus;
            player.Health = player.maxHealth; // Start fully healed
            player.AttackDamage += GlobalUpgrades.Instance.attackBonus;
        }
    }

    public void DisplayMessage(string message)
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
                int dotDamage = bot.ApplyDotEffects();
                if (dotDamage > 0)
                {
                    DisplayMessage($"Bot takes {dotDamage} status effect damage!");
                    yield return new WaitForSeconds(1f);
                }

                DisplayMessage("Bot turn to select a move!");
                yield return StartCoroutine(BotTurn());
                playerTurn = true;
            }
        }
    }

    public void PlayerAttack(int attackType)
    {
        if (playerTurn && !battleOver && !isPlayerBusy)
        {
            isPlayerBusy = true;
            StartCoroutine(PlayerTurn(attackType));
        }
    }

    public void PlayerChargeAttack()
    {
        if (playerTurn && !battleOver && chargeAttackAvailable && !isPlayerBusy)
        {
            isPlayerBusy = true;
            StartCoroutine(PlayerChargeAttackTurn());
        }
    }

    public IEnumerator PlayerHeal()
    {
        if (playerTurn && !battleOver && playerHealCounter < 2 && player.Health < player.maxHealth && !isPlayerBusy)
        {
            isPlayerBusy = true;

            player.Heal(30);
            DisplayMessage("Player healed by 30 health.");
            yield return new WaitForSeconds(1f);
            DisplayMessage($"Player's Current Health: {player.Health}");
            playerHealCounter++;
            CheckBattleOutcome();
            FindObjectOfType<BattleUI>().UpdateHealthSliders();
            playerTurn = false;

            isPlayerBusy = false;
        }
        else if (player.Health >= player.maxHealth)
        {
            DisplayMessage("Player is already at full health!");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            DisplayMessage("Player has no more heals available!");
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator PlayerTurn(int attackType)
    {
        bool isCrit = false;
        int finalDamage = 0;

        switch (attackType)
        {
            case 1:
                DisplayMessage("Player uses Blaze Overdrive!");
                yield return new WaitForSeconds(1f);
                finalDamage = player.Attack(bot, out isCrit);
                DisplayMessage($"Bot takes {finalDamage} damage!");
                if (isCrit) DisplayMessage("CRITICAL ATTACK!");
                bot.ApplyBurn(5000, 3);
                DisplayMessage("Bot is burned for 3 turns!");
                break;

            case 2:
                if (!player.attack2Used)
                {
                    DisplayMessage("Player uses Thorn Slash!");
                    yield return new WaitForSeconds(1f);
                    finalDamage = player.SecondAttack(bot, out isCrit);
                    DisplayMessage($"Bot takes {finalDamage} damage!");
                    if (isCrit) DisplayMessage("CRITICAL ATTACK!");
                    bot.ApplyPoison(3, 5);
                    DisplayMessage("Bot is poisoned (3 damage for 5 turns)!");
                    player.attack2Used = true;
                }
                else
                {
                    DisplayMessage("Thorn Slash is not available.");
                    yield return new WaitForSeconds(1f);
                }
                break;

            case 3:
                if (!player.attack3Used)
                {
                    DisplayMessage("Player uses Psycho Rift!");
                    yield return new WaitForSeconds(1f);
                    finalDamage = player.ThirdAttack(bot, out isCrit);
                    DisplayMessage($"Bot takes {finalDamage} damage!");
                    if (isCrit) DisplayMessage("CRITICAL ATTACK!");
                    player.shieldActive = true;
                    DisplayMessage("Player is shielded!");
                    player.attack3Used = true;
                }
                else
                {
                    DisplayMessage("Psycho Rift is not available.");
                    yield return new WaitForSeconds(1f);
                }
                break;

            case 4:
                if (!player.attack4Used)
                {
                    DisplayMessage("Player uses Marina Dash!");
                    yield return new WaitForSeconds(1f);
                    finalDamage = player.FourthAttack(bot, out isCrit);
                    DisplayMessage($"Bot takes {finalDamage} damage!");
                    if (isCrit) DisplayMessage("CRITICAL ATTACK!");
                    DisplayMessage($"Bot's Current Health: {bot.Health}");
                    player.attack4Used = true;
                }
                else
                {
                    DisplayMessage("Marina Dash is not available.");
                    yield return new WaitForSeconds(1f);
                }
                break;

            default:
                DisplayMessage("Unknown Attack!");
                yield return new WaitForSeconds(1f);
                break;
        }

        normalAttackCounter++;
        if (normalAttackCounter >= 3)
        {
            chargeAttackAvailable = true;
            DisplayMessage("Player's Celestial Blast is now available!");
            yield return new WaitForSeconds(1f);
        }

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);
        if (!battleOver) playerTurn = false;
        isPlayerBusy = false;
    }

    IEnumerator PlayerChargeAttackTurn()
    {
        DisplayMessage("Player uses Celestial Blast!");
        yield return new WaitForSeconds(1f);

        bool isCrit;
        int finalDamage = player.ChargeAttack(bot, out isCrit);

        DisplayMessage($"Bot takes {finalDamage} damage!");
        if (isCrit) DisplayMessage("CRITICAL ATTACK!");
        chargeAttackAvailable = false;
        normalAttackCounter = 0;

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);
        if (!battleOver) playerTurn = false;
        isPlayerBusy = false;
    }

    IEnumerator BotTurn()
    {
        if (botHealCounter < 2 && bot.Health <= 30 && !botHasHealed)
        {
            bot.Heal(30);
            DisplayMessage("Bot healed by 30 health.");
            yield return new WaitForSeconds(1f);
            DisplayMessage($"Bot's Current Health: {bot.Health}");
            botHealCounter++;
            botHasHealed = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();
        }
        else if (botChargeAttackAvailable)
        {
            if (Random.value > 0.1f)
            {
                DisplayMessage("Bot uses Celestial Blast!");
                yield return new WaitForSeconds(1f);
                bool isCrit;
                int finalDamage = bot.ChargeAttack(player, out isCrit);
                DisplayMessage($"Player takes {finalDamage} damage!");
                if (isCrit) DisplayMessage("CRITICAL ATTACK BY THE BOT!");
                botChargeAttackAvailable = false;
                botNormalAttackCounter = 0;
            }
            else
            {
                int randomAttack = Random.Range(1, 5);
                yield return PerformBotRandomAttack(randomAttack);
            }
        }
        else
        {
            int randomAttack = Random.Range(1, 5);
            yield return PerformBotRandomAttack(randomAttack);
        }

        botNormalAttackCounter++;
        if (botNormalAttackCounter >= 3)
        {
            botChargeAttackAvailable = true;
            DisplayMessage("Bot's Celestial Blast is now available!");
            yield return new WaitForSeconds(1f);
        }

        CheckBattleOutcome();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator PerformBotRandomAttack(int attackType)
    {
        bool isCrit;
        int finalDamage = 0;

        switch (attackType)
        {
            case 1:
                DisplayMessage("Bot uses Attack 1!");
                yield return new WaitForSeconds(1f);
                finalDamage = bot.Attack(player, out isCrit);
                DisplayMessage($"Player takes {finalDamage} damage!");
                if (isCrit) DisplayMessage("CRITICAL ATTACK BY THE BOT!");
                break;

            case 2:
                DisplayMessage("Bot uses Attack 2!");
                yield return new WaitForSeconds(1f);
                finalDamage = bot.SecondAttack(player, out isCrit);
                DisplayMessage($"Player takes {finalDamage} damage!");
                if (isCrit) DisplayMessage("CRITICAL ATTACK BY THE BOT!");
                break;

            case 3:
                DisplayMessage("Bot uses Attack 3!");
                yield return new WaitForSeconds(1f);
                finalDamage = bot.ThirdAttack(player, out isCrit);
                DisplayMessage($"Player takes {finalDamage} damage!");
                if (isCrit) DisplayMessage("CRITICAL ATTACK BY THE BOT!");
                break;

            case 4:
                DisplayMessage("Bot uses Attack 4!");
                yield return new WaitForSeconds(1f);
                finalDamage = bot.FourthAttack(player, out isCrit);
                DisplayMessage($"Player takes {finalDamage} damage!");
                if (isCrit) DisplayMessage("CRITICAL ATTACK BY THE BOT!");
                break;

            default:
                DisplayMessage("Unknown Bot Attack!");
                yield return new WaitForSeconds(1f);
                break;
        }

        FindObjectOfType<BattleUI>().UpdateHealthSliders();
        yield break;
    }

    void CheckBattleOutcome()
    {
        if (player.Health <= 0)
        {
            player.Health = 0;
            DisplayMessage("Player Lost The Battle!");
            battleOver = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();
            gameOverScript.gameOver();
        }
        else if (bot.Health <= 0)
        {
            bot.Health = 0;
            DisplayMessage("Bot Lost The Battle!");
            battleOver = true;
            FindObjectOfType<BattleUI>().UpdateHealthSliders();
            SceneManager.LoadScene("scene2");//Load the next scene
        }
    }
}
