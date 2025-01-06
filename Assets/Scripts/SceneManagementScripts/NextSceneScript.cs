using UnityEngine;

public class NextSceneScript : MonoBehaviour
{
    public static NextSceneScript Instance; // Singleton instance
    public int currentSceneNumber = 1;      // Start with scene1

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    /// <summary>
    /// Loads the next scene by incrementing the scene number with a fade transition.
    /// Defaults to a red fade color if none is provided.
    /// </summary>
    /// <param name="fadeColor">The color to use for the fade effect.</param>
    public void LoadNextScene(Color fadeColor = default)
    {
        // Use a default fade color if none is provided
        if (fadeColor == default)
        {
            fadeColor = Color.black;
        }

        // Increment scene number
        currentSceneNumber++;
        string nextSceneName = "scene" + currentSceneNumber; // e.g., scene2, scene3, etc.

        // If the next scene name is "Battle", fade into it just like other scenes
        if (nextSceneName == "Battle")
        {
            // Option A: Use the same fade color or pick another
            TransitionManager.Instance.LoadSceneWithColorFade("Battle", fadeColor);
            return;
        }

        // Normal case: Fade into the next "sceneX"
        TransitionManager.Instance.LoadSceneWithColorFade(nextSceneName, fadeColor);
    }
}
