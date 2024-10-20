using UnityEngine;
using UnityEngine.UI;

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

        if (healButton != null)  // Initialize heal button
        {
            healButton.onClick.AddListener(OnHealClicked);  
        }

        playerHealthSlider.maxValue = battleSystem.player.Health;
        playerHealthSlider.value = battleSystem.player.Health;

        botHealthSlider.maxValue = battleSystem.bot.Health;
        botHealthSlider.value = battleSystem.bot.Health;
    }

    void Update()
    {
        if (battleSystem != null)
        {
            chargeAttackButton.gameObject.SetActive(battleSystem.chargeAttackAvailable);

            // Disable heal button if player has used both heals
            if (healButton != null && battleSystem.playerTurn && battleSystem.playerHealCounter >= 2)
            {
                healButton.interactable = false;
            }
        }
    }

    public void UpdateHealthSliders()
    {
        playerHealthSlider.value = battleSystem.player.Health;
        botHealthSlider.value = battleSystem.bot.Health;
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
            battleSystem.PlayerHeal();  
            UpdateHealthSliders();  
        }
    }
}