using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public Button attackButton;
    public Button attackButton2;
    public Button attackButton3;
    public Button attackButton4;
    public Button chargeAttackButton;
    public Button healButton;

    public Slider playerHealthSlider;
    public Slider botHealthSlider;

    // Add references to the health text objects
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI botHealthText;

    public BattleSystem battleSystem;

    void Start()
    {
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(OnAttackClicked);
        }
        if (attackButton2 != null)
        {
            attackButton2.onClick.AddListener(OnAttack2Clicked);
        }
        if (attackButton3 != null)
        {
            attackButton3.onClick.AddListener(OnAttack3Clicked);
        }
        if (attackButton4 != null)
        {
            attackButton4.onClick.AddListener(OnAttack4Clicked);
        }

        if (chargeAttackButton != null)
        {
            chargeAttackButton.onClick.AddListener(OnChargeAttackClicked);
            chargeAttackButton.gameObject.SetActive(false);
        }

        if (healButton != null)
        {
            healButton.onClick.AddListener(OnHealClicked);
        }

        playerHealthSlider.maxValue = battleSystem.player.maxHealth;
        playerHealthSlider.value = battleSystem.player.Health;
        botHealthSlider.maxValue = battleSystem.bot.Health;
        botHealthSlider.value = battleSystem.bot.Health;

        // Update health text on start
        UpdateHealthTexts();
    }

    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (battleSystem != null)
        {
            chargeAttackButton.gameObject.SetActive(battleSystem.chargeAttackAvailable);

            if (healButton != null && battleSystem.playerTurn && battleSystem.playerHealCounter >= 2)
            {
                healButton.interactable = false;
            }

            // Disable attack buttons after one use
            attackButton2.interactable = !battleSystem.player.attack2Used;
            attackButton3.interactable = !battleSystem.player.attack3Used;
            attackButton4.interactable = !battleSystem.player.attack4Used;
        }
    }

    public void UpdateHealthSliders()
    {
        playerHealthSlider.value = battleSystem.player.Health;
        botHealthSlider.value = battleSystem.bot.Health;

        // Update the health text when the sliders are updated
        UpdateHealthTexts();
    }

    private void UpdateHealthTexts()
    {
        playerHealthText.text = $"{battleSystem.player.Health} / {playerHealthSlider.maxValue}";
        botHealthText.text = $"{battleSystem.bot.Health} / {botHealthSlider.maxValue}";
    }

    void OnAttackClicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(1);
            UpdateHealthSliders();
        }
    }

    void OnAttack2Clicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(2);
            UpdateHealthSliders();
        }
    }

    void OnAttack3Clicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(3);
            UpdateHealthSliders();
        }
    }

    void OnAttack4Clicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            battleSystem.PlayerAttack(4);
            UpdateHealthSliders();
        }
    }

    void OnChargeAttackClicked()
    {
        if (battleSystem != null && battleSystem.playerTurn && battleSystem.chargeAttackAvailable)
        {
            battleSystem.PlayerChargeAttack();
            UpdateHealthSliders();
        }
    }

    void OnHealClicked()
    {
        if (battleSystem != null && battleSystem.playerTurn)
        {
            StartCoroutine(battleSystem.PlayerHeal());
            UpdateHealthSliders();
        }
    }
}
