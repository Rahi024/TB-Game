using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Settings for the cursor visibility and lock state
    [SerializeField] private bool cursorVisible = true;
    [SerializeField] private CursorLockMode lockMode = CursorLockMode.None;

    void Start()
    {
        // Initialize cursor settings
        UpdateCursor();
    }

    void Update()
    {
        // Continuously check and enforce cursor settings
        if (Cursor.visible != cursorVisible || Cursor.lockState != lockMode)
        {
            UpdateCursor();
        }
    }

    private void UpdateCursor()
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = lockMode;
    }

    public void SetCursorSettings(bool visible, CursorLockMode lockState)
    {
        cursorVisible = visible;
        lockMode = lockState;
        UpdateCursor();
    }
}
