using UnityEngine;

public class NextSceneScript : MonoBehaviour
{
    public static NextSceneScript Instance; // Singleton instance

    public int currentSceneNumber = 1; // Start with scene1

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
    /// Loads the next scene by incrementing the scene number.
    /// </summary>
    public void LoadNextScene()
    {
        currentSceneNumber++; // Increment the scene number
        string nextSceneName = "scene" + currentSceneNumber; // Construct the next scene name
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
