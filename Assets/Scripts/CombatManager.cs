using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance; // Singleton instance
    public Player Player { get; private set; }
    public Enemy Enemy { get; private set; }
    public bool IsPlayerTurn { get; private set; }
    private UIManager UIManager;

    private void Awake() //Ensure only ne instance of coombat manager
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartCombat()
    {
        Player = FindFirstObjectByType<Player>();
        Enemy = FindFirstObjectByType<Enemy> ();
        UIManager = FindFirstObjectByType<UIManager>();

        UIManager.StartCombat();
        // Initialize the combat, drawing starting hands for both players
        Player.DrawCard();
        Enemy.DrawCard();
        // Update UI elements (health bars, mana displays) here if needed
    }

    public void EndTurn()
    {
        // Switch the current turn to the other player
        IsPlayerTurn = !IsPlayerTurn;
        // Update UI elements (turn indicator) here if needed

        // If it's the enemy's turn, make the enemy play a card automatically
        if (!IsPlayerTurn)
        {
            // Logic for the enemy to play a card
            if (Enemy.Hand.Count > 0)
            {
                Card enemyCard = Enemy.Hand[0]; //play the first card in hand
                HandleCardPlayed(enemyCard);
            }
        }
    }

    public void CheckWinCondition()
    {
        // Check if either player has won the combat
        if (Player.Health <= 0)
        {
            // Handle player defeat
            UIManager.LogCombatEvent("Player defeated!");
        }
        else if (Enemy.Health <= 0)
        {
            // Handle enemy defeat
            UIManager.LogCombatEvent("Enemy defeated!");
        }
    }

    public void HandleCardPlayed(Card card)
    {
        // Process the effect of the played card
        card.PlayCard(Player, Enemy);
        // Update UI elements (combat log) here if needed
        EndTurn(); // End the current turn after playing a card
    }

    

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
