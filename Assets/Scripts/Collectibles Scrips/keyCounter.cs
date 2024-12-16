using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Ensure this is included if you're using TextMeshPro

public class keyCounter : MonoBehaviour
{
    public int keyCount; // Counter for keys
    public TextMeshProUGUI keyCountText; // Reference to the Text component

    public void Start()
    {
        // Initialize the display
        UpdateKeyCountDisplay();
    }

    // Method to increment the key count
    public void IncrementKeyCount()
    {
        keyCount++; // Increment the key count
        UpdateKeyCountDisplay(); // Update the display
    }

    // Method to update the text display
    void UpdateKeyCountDisplay()
    {
        if (keyCountText != null)
        {
            keyCountText.text = "Keys: " + keyCount; // Update the text
        }
    }
}
