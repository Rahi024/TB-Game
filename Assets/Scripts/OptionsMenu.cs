using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider brightnessSlider;
    public Slider audioSlider;
    public Slider sensitivitySlider;
    public Toggle invertCameraToggle;
    public GameObject optionsMenuCanvas;

    private float defaultBrightness = 1.0f;
    private float defaultAudio = 1.0f;
    private float defaultSensitivity = 1.0f;
    private bool defaultInvertCamera = false;

    void Start()
    {
        // Set default slider and toggle values
        brightnessSlider.value = defaultBrightness;
        audioSlider.value = defaultAudio;
        sensitivitySlider.value = defaultSensitivity;
        invertCameraToggle.isOn = defaultInvertCamera;

        // Add listeners to sliders and toggle
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        audioSlider.onValueChanged.AddListener(SetAudioVolume);
        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        invertCameraToggle.onValueChanged.AddListener(SetInvertCamera);

        // Hide the options menu by default
        optionsMenuCanvas.SetActive(false);
    }

    public void SetBrightness(float brightness)
    {
        // Adjust brightness by changing the ambient light intensity
        RenderSettings.ambientLight = Color.white * brightness;
    }

    public void SetAudioVolume(float volume)
    {
        // Adjust audio volume (global)
        AudioListener.volume = volume;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        // Save sensitivity setting (for use in your player controller script)
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

    public void SetInvertCamera(bool isInverted)
    {
        // Save invert camera setting
        PlayerPrefs.SetInt("InvertCamera", isInverted ? 1 : 0);
        Debug.Log("Invert Camera: " + (isInverted ? "Enabled" : "Disabled"));
    }

    public void OpenOptionsMenu()
    {
        optionsMenuCanvas.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenuCanvas.SetActive(false);
    }
}
