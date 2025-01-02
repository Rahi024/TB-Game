using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    // Reference to the CharacterController component that will handle the player's movement
    public CharacterController controller;
    public Animator animator;

    public float baseSpeed = 12f;

    public float gravity = -9.81f;

    public float jumpHeight = 3f;

    public float sprintSpeed = 5f;

    float speedBoost = 1f;

    Vector3 velocity;

    void Start()
    {
        Cursor.visible = true; // Ensure the cursor is visible
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is grounded and reset the vertical velocity when touching the ground
        if (controller.isGrounded && velocity.y < 0)
        {
            // Set a small negative value to keep the player grounded
            velocity.y = -2f;
        }

        // Get player input for movement on the horizontal (x) and vertical (z) axes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Check if the "Fire3" button (usually the sprint button) is pressed to boost speed
        if (Input.GetButton("Fire3"))
            speedBoost = sprintSpeed;  // Apply sprint speed boost
        else
            speedBoost = 1f;  // Normal speed when not sprinting

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (baseSpeed + speedBoost) * Time.deltaTime);

        // Check if the jump button is pressed and the player is grounded
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            // Calculate the velocity needed to achieve the desired jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity over time to simulate falling when not grounded
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Animations
        animator.SetBool("isRun", Input.GetAxisRaw("Vertical") != 0);
    }
}
