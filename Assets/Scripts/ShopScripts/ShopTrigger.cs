using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;       // The ShopPanel we want to enable/disable
    [SerializeField] private GameObject interactPrompt; // A small UI “Press F to open shop” prompt
    [SerializeField] private MonoBehaviour cameraController; // Reference to the camera controller script

    private bool isPlayerInside = false;

    private void Start()
    {
        // Ensure the shop UI and interact prompt start hidden
        if (shopUI != null) shopUI.SetActive(false);
        if (interactPrompt != null) interactPrompt.SetActive(false);

        // Ensure the camera controller is enabled at start
        if (cameraController != null)
            cameraController.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            if (interactPrompt != null)
                interactPrompt.SetActive(false);

            // Hide shop if open
            if (shopUI != null)
                shopUI.SetActive(false);

            // Re-enable the camera controller when exiting
            if (cameraController != null)
                cameraController.enabled = true;
        }
    }

    private void Update()
    {
        // If the player is inside the trigger and presses F, toggle the shop UI
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            if (shopUI != null)
            {
                bool isActive = shopUI.activeSelf;
                shopUI.SetActive(!isActive); // Toggle shop

                if (shopUI.activeSelf)
                {
                    // Dynamically reassign the coin text reference to CoinManager
                    UpdateCoinTextReferenceInShop();

                    // Disable the camera controller
                    if (cameraController != null)
                        cameraController.enabled = false;
                }
                else
                {
                    // Re-enable the camera controller
                    if (cameraController != null)
                        cameraController.enabled = true;
                }
            }
        }
    }

    private void UpdateCoinTextReferenceInShop()
    {
        // Dynamically set the coinText reference in CoinManager to ensure it's up-to-date
        var coinDisplay = shopUI.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (coinDisplay != null)
        {
            CoinManager.Instance.SetCoinTextReference(coinDisplay);
        }
        else
        {
            Debug.LogError("ShopTrigger: No coinText UI found in shop!");
        }
    }
}
