using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float gravity = -9.8f; // Gravity force (negative for falling down)
    public float jumpForce = 5f; // Upward force when pressing the space bar
    public float maxFallSpeed = -20f; // Maximum falling forwordSpeed
    public float maxHeight = 20F;
    public float forwardSpeed = 0f; // How fast the boat moves forward
    public float forwardSpeedDampener = 20f; // Controls the speed of the boat depending on the height
    public float horizontalSpeed = 0f; // How fast the boat moves left or right

    private CharacterController characterController;
    private Vector3 velocity; // Current velocity of boat
    private bool coolDown = false; // Only allow one jump every 0.3 seconds

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        #region Vertical Movement
        // Apply gravity to velocity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0; // Reset velocity if grounded
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity
        }

        // Limit the fall forwordSpeed to maxFallSpeed
        velocity.y = Mathf.Max(velocity.y, maxFallSpeed);

        // Check for the space bar input to "float" the boat
        if (Input.GetKeyDown(KeyCode.Space) && !coolDown && transform.position.y < maxHeight)
        {
        Debug.Log("Up!!");
            coolDown = true; // Set the cooldown flag
            velocity.y = jumpForce; // Apply upward force
            StartCoroutine(CooldownRefresh()); // Start the cooldown timer
        }

        // Move the CharacterController based on velocity
        characterController.Move(velocity * Time.deltaTime);
        #endregion

        #region Forward Movement
        // Make forward movement faster the farther the boat is from the ground
        forwardSpeed = Mathf.Max(0f, 1f + transform.position.y / 20f);

        // Constant forward movement
        characterController.Move(Vector3.forward * forwardSpeed * Time.deltaTime);
        #endregion

        #region Horizontal Movement
        // Move the boat with the arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right Arrow");
            horizontalSpeed = 1f;
            characterController.Move(Vector3.right * horizontalSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow)) // Move the boat right with Left Arrow
        {
            Debug.Log("Left Arrow");
            horizontalSpeed = 1f;
            characterController.Move(Vector3.right * horizontalSpeed * Time.deltaTime);
        } else
        {
            horizontalSpeed = 0f;
        }
        #endregion
    }

    private IEnumerator CooldownRefresh() {
        yield return new WaitForSeconds(0.3f);
        coolDown = false;
    } 
}