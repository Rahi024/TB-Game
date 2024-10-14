using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }

    public Image EnemyHealthBar;
    public Deck Deck { get; private set; }
    public List<Card> Hand { get; private set; } = new List<Card>();
    private UIManager UIManager;

    public void DrawCard()
    {
        // Logic to draw a card from the Deck and add it to the Hand
        Card drawnCard = Deck.DrawCard();
        Hand.Add(drawnCard);
        UIManager.LogCombatEvent("Enemy drew a card: " + drawnCard.Name);
    }

    //Eventaully add ability to out card back

    public void PlayCard(Card card)
    {
        // Logic to play a card from the Hand, applying its effect
        card.PlayCard(CombatManager.Instance.Player, this); // Assuming you have a CombatManager singleton
        Hand.Remove(card); // Remove the card from the hand after playing
        
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage; // Reduce health
        EnemyHealthBar.fillAmount = (float)Health / MaxHealth;
        if (Health <= MaxHealth * 0.2f)
        {
            Color color = EnemyHealthBar.color;
            color.a = 0.5f; // Set transparency to 50%
            EnemyHealthBar.color = color;
            UIManager.LogCombatEvent("Enemy took damage: " + Damage + ", Health: " + Health);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        EnemyHealthBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
