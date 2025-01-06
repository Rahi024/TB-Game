using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // for Slider

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    [Header("Background Music & Brightness")]
    public AudioSource bgMusicSource; // Assign the GameObject's AudioSource with background music
    public Slider volumeSlider;       // Assign via Inspector
    public Slider brightnessSlider;   // Assign via Inspector

    void Start()
    {
        // Initialize sliders with current values
        if (bgMusicSource != null)
        {
            volumeSlider.value = bgMusicSource.volume; 
        }

        // For brightness, let's start at "1" = fully lit (rendering white ambient light)
        brightnessSlider.value = 1f; 
    }

    void Update()
    {
        // Let ESC toggle pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    // ==============================
    // SLIDER CALLBACKS
    // ==============================

    // 1) Volume: only affects the bgMusicSource's AudioSource
    public void OnVolumeSliderChanged(float newVolume)
{
    Debug.Log($"Volume Slider Changed: {newVolume}");
    if (bgMusicSource != null)
    {
        bgMusicSource.volume = newVolume;
        Debug.Log($"AudioSource Volume Set To: {bgMusicSource.volume}");
    }
}

    // 2) Brightness: adjust ambient light to make the scene darker/brighter
    public void OnBrightnessSliderChanged(float newBrightness)
    {
        // Simply set ambientLight to a shade of gray. 
        // 1 means pure white (fully lit), 0 means black (completely dark).
        RenderSettings.ambientLight = new Color(newBrightness, newBrightness, newBrightness, 1f);
    }
}
