using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public float cutsceneDuration = 25f; // Duration of the cutscene in seconds
    public string nextSceneName = "FirstScene"; // Name of the scene to load

    private void Start()
    {
        // Start a timer to change the scene after the cutscene ends
        Invoke("LoadNextScene", cutsceneDuration);
    }

    private void LoadNextScene()
    {
        // Load the specified scene
        SceneManager.LoadScene("Scene1");
    }
}
