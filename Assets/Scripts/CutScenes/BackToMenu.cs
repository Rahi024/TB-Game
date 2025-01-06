using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneToMenu : MonoBehaviour
{
    [Header("Cutscene Settings")]
    [Tooltip("How long the cutscene lasts in seconds.")]
    [SerializeField] private float cutsceneDuration = 10f;

    [Header("Scene Settings")]
    [Tooltip("Name of the scene to load after the cutscene.")]
    [SerializeField] private string menuSceneName = "MainMenu";

    private void Start()
    {
        StartCoroutine(WaitThenLoadMenu());
    }

    private System.Collections.IEnumerator WaitThenLoadMenu()
    {
        // Wait for the cutscene to finish
        yield return new WaitForSeconds(cutsceneDuration);

        // Load the menu scene
        SceneManager.LoadScene(menuSceneName);
    }
}
