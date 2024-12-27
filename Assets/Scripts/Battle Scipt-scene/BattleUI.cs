using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    // Buttons for different player actions (attacks, charge, heal)
    public Button attackButton;
    public Button attackButton2;
    public Button attackButton3;
    public Button attackButton4;
    public Button chargeAttackButton;
    public Button healButton;

    // Sliders to display health for player and bot
    public Slider playerHealthSlider;
    public Slider botHealthSlider;

    // Text elements to show the current health as a numerical value
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI botHealthText;

    // Reference to the main battle system
    public BattleSystem battleSystem;

    // Start is called before the first frame update
    void Start()
    {
            // At this point, `battleSystem.player.maxHealth` is already upgraded to 210
        playerHealthSlider.maxValue = battleSystem.player.maxHealth;  // Now becomes 210
        playerHealthSlider.value    = battleSystem.player.Health;     // Also 210


        // Attach button click listeners to respective functions
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackClicked); // Attack 1
        }
        if (attackButton2 != null)
        {
            attackButton2.onClick.AddListener(OnAttack2Clicked); // Attack 2
        }
        if (attackButton3 != null)
        {
            attackButton3.onClick.AddListener(OnAttack3Clicked); // Attack 3
        }
        if (attackButton4 != null)
        {
            attackButton4.onClick.AddListener(OnAttack4Clicked); // Attack 4
        }

        // Attach charge attack listener and set its initial state to inactive
        if (chargeAttackButton != null)
        {
            chargeAttackButton.onClick.AddListener(OnChargeAttackClicked);
            chargeAttackButton.gameObject.SetActive(false); // Charge attack is initially unavailable
        }

        // Attach heal button listener
        if (healButton != null)
        {
            healButton.onClick.AddListener(OnHealClicked);
        }

        // Set the max values and current values for the health sliders (player and bot)
        playerHealthSlider.maxValue = battleSystem.player.maxHealth;
        playerHealthSlider.value = battleSystem.player.Health;
        botHealthSlider.maxValue = battleSystem.bot.Health;
        botHealthSlider.value = battleSystem.bot.Health;

        // Initialize the health text display
        UpdateHealthTexts();
    }

    // Update is called once per frame
    void Update()
    {
        // Make the mouse cursor visible and unlocked in the game window
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Check the battle system to update UI states
        if (battleSystem != null)
        {
            chargeAttackButton.gameObject.SetActive(battleSystem.chargeAttackAvailable);

            // Disable heal button if player has used up their heals
            if (healButton != null && battleSystem.playerTurn && battleSystem.playerHealCounter >= 2)
            {
                healButton.interactable = false;
            }

            // Disable attack buttons if they have been used once
            attackButton2.interactable = !battleSystem.player.attack2Used;
            attackButton3.interactable = !battleSystem.player.attack3Used;
            attackButton4.interactable = !battleSystem.player.attack4Used;
        }
    }

    // Update health sliders based on current player and bot health values
    public void UpdateHealthSliders()
    {
        playerHealthSlider.value = battleSystem.player.Health; // Update player's health slider
        botHealthSlider.value = battleSystem.bot.Health; // Update bot's health slider

        // Also update the text to show the numeric health values
        UpdateHealthTexts();
    }

    // Update the health text for player and bot to display current and max health
    private void UpdateHealthTexts()
    {
        playerHealthText.text = $"{battleSystem.player.Health} / {playerHealthSlider.maxValue}";
        botHealthText.text = $"{battleSystem.bot.Health} / {botHealthSlider.maxValue}";
    }

    // When the first attack button is clicked, trigger Player Attack 1
    void OnAttackClicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(1); // Player chooses Attack 1
            UpdateHealthSliders(); // Update UI after the attack
        }
    }

    // When the second attack button is clicked, trigger Player Attack 2
    void OnAttack2Clicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(2); // Player chooses Attack 2
            UpdateHealthSliders(); // Update UI after the attack
        }
    }

    // When the third attack button is clicked, trigger Player Attack 3
    void OnAttack3Clicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(3); // Player chooses Attack 3
            UpdateHealthSliders(); // Update UI after the attack
        }
    }

    // When the fourth attack button is clicked, trigger Player Attack 4
    void OnAttack4Clicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(4); // Player chooses Attack 4
            UpdateHealthSliders(); // Update UI after the attack
        }
    }

    // When the charge attack button is clicked, trigger Player's charge attack
    void OnChargeAttackClicked()
    {
        if (battleSystem != null && battleSystem.playerTurn && battleSystem.chargeAttackAvailable)
        {
            battleSystem.PlayerChargeAttack(); // Player uses charge attack
            UpdateHealthSliders(); // Update UI after the charge attack
        }
    }

    // When the heal button is clicked, trigger Player's heal action
    void OnHealClicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            StartCoroutine(battleSystem.PlayerHeal()); // Player heals
            UpdateHealthSliders(); // Update UI after healing
        }
    }
}
