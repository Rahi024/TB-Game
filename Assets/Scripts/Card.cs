using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Cost { get; private set; } // Experience cost to play the card (int)
    public int Attack { get; private set; } // Attack value of the card (int)
    private UIManager UIManager;
    public void PlayCard(Player player, Enemy enemy)
    {
        // Check if the player has enough experience to play the card
        if (player.Exp >= Cost)
        {
            // Deduct the cost from the player's experience
            player.Exp -= Cost;

            // Apply the card's effect
            if (Attack > 0)
            {
                // Attack card: deal damage to the enemy
                enemy.TakeDamage(Attack);
            }
            else if (Attack < 0)
            {
                // Heal card: heal the player
                player.Heal(-Attack);
            }
        }
        else
        {
            UIManager.LogCombatEvent("Not enough experience to play this card.");
        }
    }

}

