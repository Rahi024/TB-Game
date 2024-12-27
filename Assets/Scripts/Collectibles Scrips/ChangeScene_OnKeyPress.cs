using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_OnKeyPress : MonoBehaviour
{
    [SerializeField] private GameObject uiElement; // "Press F to pick up" UI
    private keyCounter kc; // Reference to the keyCounter in the scene

    private void Start()
    {
        // Find the keyCounter in this scene
        kc = FindObjectOfType<keyCounter>();
        if (kc == null)
        {
            Debug.LogError("keyCounter instance not found in the scene!");
        }

        // Hide the UI element initially if it exists
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the "Press F" UI
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }

            // Pick up key on F press
            if (Input.GetKeyDown(KeyCode.F) && kc != null)
            {
                kc.IncrementKeyCount();
                Destroy(transform.parent.gameObject);  // Remove the key object

                if (uiElement != null)
                {
                    Destroy(uiElement);
                }
            }

            // If more than 3 keys, reset keys & load "Battle" scene
            if (kc != null && kc.keyCount > 3)
            {
                kc.keyCount = 0;    // or just rely on a new scene's keyCounter = 0
                SceneManager.LoadScene("Battle");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }
}
