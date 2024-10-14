using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : ScriptableObject
{
    public List<Card> Cards { get; private set; } = new List<Card>();

    public Card DrawCard()
    {
        // Logic to remove and return the top card from the Cards list
        if (Cards.Count > 0)
        {
            Card topCard = Cards[0];
            Cards.RemoveAt(0);
            return topCard;
        }
        else
        {
            return null; // Or handle the case of an empty deck
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
