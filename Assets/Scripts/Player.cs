using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class Player : MonoBehaviour
{
    public UnityEngine.Vector2 moveValue;
    public float moveSpeed = 5f;
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }
    public Image PlayerHealthBar;
    public int MaxExp { get; private set; }
    public int Exp { get; set; }
    public Deck Deck { get; private set; }
    private UIManager UIManager;

    public List<Card> Hand { get; private set; } = new List<Card>();


    void OnMove(InputValue value)
    { //method captures input
        moveValue = value.Get<UnityEngine.Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arena")
        {
            CombatManager.Instance.StartCombat();
        }
    }

    public void DrawCard()
    {
        // Logic to draw a card from the Deck and add it to the Hand
        Card drawnCard = Deck.DrawCard();
        Hand.Add(drawnCard);
        UIManager.LogCombatEvent("Player drew a card: " + drawnCard.Name);
    }

    //Eventaully add ability to out card back

    public void PlayCard(Card card)
    {
        // Logic to play a card from the Hand, applying its effect
        card.PlayCard(this, CombatManager.Instance.Enemy); // Assuming you have a CombatManager singleton
        //Exp -= CombatMCost;
        Hand.Remove(card); // Remove the card from the hand after playing
        UIManager.LogCombatEvent("Player played a card: " + card.Name);

    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage; // Reduce health
        PlayerHealthBar.fillAmount = (float)Health / MaxHealth;
        if (Health <= MaxHealth * 0.2f)
        {
            PlayerHealthBar.color = Color.red; // Turn red when health is low
        }
        UIManager.LogCombatEvent("Player took damage: " + Damage + ", Health: " + Health);
    }

    public void Heal(int amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        PlayerHealthBar.fillAmount = (float)Health / MaxHealth;
        UIManager.LogCombatEvent("Player healed: " + amount + ", Health: " + Health);
    }


    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        PlayerHealthBar.fillAmount = 1f;
        Exp = MaxExp;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get input from the user
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the new position
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime;

        // Apply the movement to the cylinder's position
        transform.Translate(movement); 

    }
}
