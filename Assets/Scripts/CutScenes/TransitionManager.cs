using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance; 
    [Header("Assign a single UI Image for ALL fades")]
    public Image fadeImage;                // Use one fade image everywhere
    public float fadeDuration = 1f;        // Duration of the fade effect

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

    private void Start()
    {
        // Start with a fade-in effect (default to black)
        StartCoroutine(FadeIn(fadeImage, Color.black));
    }

    /// <summary>
    /// Loads the specified scene with a fade effect in the given color.
    /// </summary>
    public void LoadSceneWithColorFade(string nextSceneName, Color fadeColor)
    {
        StartCoroutine(TransitionWithColor(nextSceneName, fadeImage, fadeColor));
    }

    /// <summary>
    /// (Optional) Example method if you want a quick way to load the
    /// Battle scene with a specific color (could be black, red, etc.).
    /// </summary>
    public void LoadBattleScene()
    {
        // Example: Fade to black if loading the Battle scene
        StartCoroutine(TransitionWithColor("Battle", fadeImage, Color.black));
    }

    /// <summary>
    /// Fade In Coroutine - starts fully opaque, goes to transparent.
    /// </summary>
    private IEnumerator FadeIn(Image image, Color color)
    {
        float elapsedTime = fadeDuration;
        
        // Make sure the fade image is active
        image.gameObject.SetActive(true);
        
        // Start fully opaque
        color.a = 1f;
        image.color = color;

        while (elapsedTime > 0f)
        {
            elapsedTime -= Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); 
            image.color = color;
            yield return null;
        }

        // Once fully transparent, you can disable the fade image
        image.gameObject.SetActive(false);
    }

    /// <summary>
    /// Fade Out Coroutine - starts fully transparent, goes to opaque.
    /// </summary>
    private IEnumerator FadeOut(Image image, Color color)
    {
        float elapsedTime = 0f;
        
        // Make sure the fade image is active
        image.gameObject.SetActive(true);

        // Start fully transparent
        color.a = 0f;
        image.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); 
            image.color = color;
            yield return null;
        }
    }

    /// <summary>
    /// Main transition coroutine using only one fade image.
    /// </summary>
    private IEnumerator TransitionWithColor(string nextSceneName, Image image, Color fadeColor)
    {
        // Fade Out (transparent -> opaque)
        yield return StartCoroutine(FadeOut(image, fadeColor));

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);

        // Fade In (opaque -> transparent)
        yield return StartCoroutine(FadeIn(image, fadeColor));
    }
}
