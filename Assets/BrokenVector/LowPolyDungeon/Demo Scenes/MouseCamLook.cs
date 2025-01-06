using UnityEngine;
using Cinemachine;

public class CharacterFaceCameraDirection : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;            // The transform that should rotate (your character model/root)
    public CinemachineFreeLook freeLookCam; // Drag your Cinemachine FreeLook here

    [Header("Settings")]
    public float rotationSpeed = 5f; // How quickly to slerp the character’s rotation

    void LateUpdate()
    {
        if (playerBody == null || freeLookCam == null) return;

        // 1) Get the camera’s forward direction from the FreeLookCam
        // Note: freeLookCam.transform is the actual camera transform at runtime
        Vector3 camForward = freeLookCam.transform.forward;

        // 2) Ignore any vertical tilt by setting y = 0
        camForward.y = 0f;
        camForward.Normalize();

        // 3) If there is a valid direction, smoothly rotate the player to face it
        if (camForward.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(camForward, Vector3.up);
            playerBody.rotation = Quaternion.Slerp(
                playerBody.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
