using UnityEngine;
using UnityEngine.UI;

public class PanelFlicker : MonoBehaviour
{
    public Image panelImage; // The UI Image component of your panel
    public Color startColor = Color.black; // Starting color of the panel
    public Color endColor = Color.gray; // Glowing color of the panel
    public float pulseSpeed = 2f; // Speed of the pulsating effect

    private bool isIncreasing = true; // Tracks whether the glow is increasing or decreasing
    private float t = 0f; // Interpolation factor

    private void Update()
    {
        if (panelImage == null)
        {
            return; // Ensure the Image component is set
        }

        // Adjust the interpolation factor `t` over time
        if (isIncreasing)
        {
            t += Time.deltaTime * pulseSpeed; // Increase `t` to brighten
            if (t >= 1f)
            {
                t = 1f; // Clamp to maximum
                isIncreasing = false; // Start decreasing
            }
        }
        else
        {
            t -= Time.deltaTime * pulseSpeed; // Decrease `t` to dim
            if (t <= 0f)
            {
                t = 0f; // Clamp to minimum
                isIncreasing = true; // Start increasing
            }
        }

        // Interpolate between startColor and endColor
        panelImage.color = Color.Lerp(startColor, endColor, t);
    }
}
