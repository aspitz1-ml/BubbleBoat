using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float gravity = -9.8f; // Gravity force (negative for falling down)
    public float jumpForce = 5f; // Upward force when pressing the space bar
    public float maxFallSpeed = -20f; // Maximum falling forward Speed
    public float maxHeight = 20F;
    public float forwardSpeed = 10f; // How fast the boat moves forward
    public float forwardSpeedBoost = 1f; // Adds boost on vertical movement key input
    public float horizontalSpeed = 0f; // How fast the boat moves left or right

    public GameObject DefaultLookAt; // Neutral look at point
    public GameObject LookLeftPoint; // Look at point when moving left
    public GameObject LookRightPoint; // Look at point when moving right
    public GameObject LookUpPoint; // Look at point when moving up
    public GameObject LookAt; // Current look at point

    private CharacterController characterController;
    private Vector3 velocity; // Current velocity of boat
    private bool coolDown = false; // Only allow one jump every 0.3 seconds

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
        characterController.Move(Vector3.forward * forwardSpeed * Time.deltaTime );
        // Look neutral by default
        LookAt = DefaultLookAt;

        // Add initial fuel to the boat if Fuel singleton exists
        if (Fuel.Instance != null)
        {
            Fuel.Instance.AddFuel(Fuel.Instance.maxFuel);
            Debug.Log("Fuel added to boat");
        } else {
            Debug.Log("Fuel instance not found");
        }
    }

    void Update()
    {
        #region Fuel Check
        // Stop movement if out of fuel
        if (Fuel.Instance != null && Fuel.Instance.currentFuel <= 0)
        {
            Debug.Log("Out of fuel! Boat cannot move.");
            forwardSpeed = 0; // Stop forward movement
            return; // Skip further processing
        }
        #endregion

        #region Fuel Consumption
        // Consume fuel over time
        if (Fuel.Instance != null)
        {
            Fuel.Instance.currentFuel -= Fuel.Instance.fuelConsumptionRate * Time.deltaTime;
            Fuel.Instance.currentFuel = Mathf.Max(Fuel.Instance.currentFuel, 0); // Clamp to 0
        }

        if (Fuel.Instance != null && Fuel.Instance.currentFuel <= 0)
        {
            Debug.Log("Out of fuel! Boat cannot move.");
            forwardSpeed = 0; // Stop forward movement
            return; // Skip further processing
        }
        
        #endregion

        // Debug.Log("Forward Speed: " + forwardSpeed);
        #region Vertical Movement
        // Apply gravity to velocity
        velocity.y += gravity * Time.deltaTime; // Apply gravity

        // Limit the fall to maxFallSpeed
        velocity.y = Mathf.Max(velocity.y, maxFallSpeed);

        // Check for the space bar input to "float" the boat
        if (Input.GetKeyDown(KeyCode.Space) && !coolDown && transform.position.y < maxHeight)
        {
            Debug.Log("Up!!");
            coolDown = true; // Set the cooldown flag
            velocity.y = jumpForce; // Apply upward force
            // Give boost to forward speed
            forwardSpeed += forwardSpeedBoost;
            // remove a bit of extra fuel when jumping
            Fuel.Instance.AddFuel(-5f);

            // Look Up!
            LookAt = LookUpPoint;
            StartCoroutine(ForwardSpeedRefresh()); // Start the forward speed timer
            StartCoroutine(CooldownRefresh()); // Start the cooldown timer
        }

        // Move the CharacterController based on velocity
        characterController.Move(velocity * Time.deltaTime);
        #endregion

        #region Forward Movement
        // Constant forward movement
        // TODO: This moves forward even when the boat is grounded - The boat grounded is game over but it probably should not move on the ground
        characterController.Move(Vector3.forward * forwardSpeed * Time.deltaTime );
        #endregion

        #region Horizontal Movement
        // Move the boat with the arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right Arrow");
            horizontalSpeed = 10f;
            // Look Right!
            LookAt = LookRightPoint;
            characterController.Move(Vector3.right * horizontalSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow)) // Move the boat right with Left Arrow
        {
            Debug.Log("Left Arrow");
            horizontalSpeed = 10f;
            // Look Left!
            LookAt = LookLeftPoint;
            characterController.Move(Vector3.left * horizontalSpeed * Time.deltaTime);
        } else
        {
            horizontalSpeed = 0f;
        }
        #endregion

        #region Smooth Rotation
        if (LookAt != null)
        {
            Debug.Log("Looking at: " + LookAt);
            Quaternion LookAtRotation = Quaternion.LookRotation(LookAt.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, LookAtRotation, Time.deltaTime * 2f);
        }
        #endregion
    }

    private IEnumerator ForwardSpeedRefresh() {
        yield return new WaitForSeconds(0.15f);
        forwardSpeed = 10f;
    }

    private IEnumerator CooldownRefresh() {
        yield return new WaitForSeconds(0.3f);
        coolDown = false;
    } 
}