public class MouseCamLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse sensitivity for looking around
    public Transform playerBody; // Reference to the player's body for rotation
    float xRotation = 0f; // Tracks vertical rotation of the camera

    void Start()
    {
        // Locks the cursor to the center of the screen and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input for horizontal (X) and vertical (Y) movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust vertical camera rotation based on mouse Y input
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); // Rotate the player horizontally based on mouse X input
    }
}
