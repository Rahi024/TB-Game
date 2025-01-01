using UnityEngine;
using TMPro;

public class SceneInitializer : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Reference to the coin text in this scene

    private void Start()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.SetCoinTextReference(coinText); // Set the new text reference
        }
    }
}
