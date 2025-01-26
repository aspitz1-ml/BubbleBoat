using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float gravity = -9.8f; // Gravity force (negative for falling down)
    public float jumpForce = 5f; // Upward force when pressing the space bar
    public float maxFallSpeed = -20f; // Maximum falling forward Speed
    public float maxHeight = 50F; // Maximum height the boat can reach
    public float forwardSpeed = 10f; // How fast the boat moves forward
    public float forwardSpeedBoost = 1f; // Adds boost on vertical movement key input
    public float horizontalSpeed = 0f; // How fast the boat moves left or right
    public float rotationSpeed = 5f; // How fast the boat rotates when turning
    public float maxRotationAngle = 30f; // Maximum angle to tilt when turning
    public float maxTiltAngle = 10f; // Maximum angle to tilt left/right
    public float maxVerticalTiltAngle = 15f; // Maximum angle to tilt upward/downward
    public DamageFlash damageFlash; // Reference to the DamageFlash script
    public GameOver gameOver; // Reference to the GameOver script

    private CharacterController characterController;
    private Vector3 velocity; // Current velocity of boat
    private bool coolDown = false; // Only allow one jump every 0.3 seconds
    private float targetYRotation = 0f; // Target rotation angle on the Y-axis
    private float currentYRotation = 0f; // Current rotation angle for smooth interpolation
    private float targetXRotation = 0f; // Target rotation angle on the X-axis
    private float currentXRotation = 0f; // Current rotation angle for smooth interpolation
    private float targetZRotation = 0f; // Target rotation angle on the Z-axis (upward/downward tilt)
    private float currentZRotation = 0f; // Current Z-axis rotation for smooth interpolation

    void Start()
    {
        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
        characterController.Move(Vector3.forward * forwardSpeed * Time.deltaTime);
        damageFlash = GetComponent<DamageFlash>();
        GameObject gameOverObj = GameObject.Find("GameOverText");
        if (gameOverObj != null)
        {
            Debug.Log("Hit OBJECT");
            gameOver = gameOverObj.GetComponent<GameOver>();
        }
        

        // Add initial fuel to the boat if Fuel singleton exists
        if (Fuel.Instance != null)
        {
            Fuel.Instance.AddFuel(Fuel.Instance.maxFuel);
            Debug.Log("Fuel added to boat");
        }
        else
        {
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
            gameOver.OnGameOver();

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

        #region Vertical Movement
        // Apply gravity to velocity
        velocity.y += gravity * Time.deltaTime; // Apply gravity

        // Limit the fall to maxFallSpeed
        velocity.y = Mathf.Max(velocity.y, maxFallSpeed);

        // Check for the space bar input to "float" the boat
        if (Input.GetKeyDown(KeyCode.Space) && !coolDown && transform.position.y < maxHeight)
        {
            // Debug.Log("Up!!");
            coolDown = true; // Set the cooldown flag
            velocity.y = jumpForce; // Apply upward force
            // Give boost to forward speed
            forwardSpeed += forwardSpeedBoost;
            // remove a bit of extra fuel when jumping
            Fuel.Instance.AddFuel(-5f);

            StartCoroutine(ForwardSpeedRefresh()); // Start the forward speed timer
            StartCoroutine(CooldownRefresh()); // Start the cooldown timer
        }

        // Move the CharacterController based on velocity
        characterController.Move(velocity * Time.deltaTime);

        // Tilt upward when moving up, and downward when falling
        if (velocity.y > 0)
        {
            targetZRotation = -maxVerticalTiltAngle; // Tilt upward
        }
        else if (velocity.y < 0)
        {
            targetZRotation = maxVerticalTiltAngle; // Tilt downward
        }
        else
        {
            targetZRotation = 0f; // Neutral when not moving up or down
        }
        #endregion

        #region Forward Movement
        // Constant forward movement
        characterController.Move(Vector3.forward * forwardSpeed * Time.deltaTime);
        #endregion

        #region Horizontal Movement
        // Move the boat with the arrow keys or A/D keys
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // Debug.Log("Right Arrow");
            horizontalSpeed = 10f;
            characterController.Move(Vector3.right * horizontalSpeed * Time.deltaTime);
            targetYRotation = maxRotationAngle; // Turn to the left
            targetXRotation = -maxTiltAngle; // Tilt to the left
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // Debug.Log("Left Arrow");
            horizontalSpeed = 10f;
            characterController.Move(Vector3.left * horizontalSpeed * Time.deltaTime);
            targetYRotation = -maxRotationAngle; // Turn to the right
            targetXRotation = maxTiltAngle; // Tilt to the right
        }
        else
        {
            horizontalSpeed = 0f;
            targetYRotation = 0f; // Straighten the boat when not turning
            targetXRotation = 0f; // Straighten the boat when not turning
        }
        #endregion

        #region Smooth Rotation
        // Smoothly interpolate the current rotation towards the target rotation
        currentYRotation = Mathf.Lerp(currentYRotation, targetYRotation, Time.deltaTime * rotationSpeed);
        currentXRotation = Mathf.Lerp(currentXRotation, targetXRotation, Time.deltaTime * rotationSpeed);
        currentZRotation = Mathf.Lerp(currentZRotation, targetZRotation, Time.deltaTime * rotationSpeed);

        // Apply the calculated rotation to the boat
        transform.rotation = Quaternion.Euler(currentZRotation, currentYRotation, currentXRotation);
        #endregion
    }

    private IEnumerator ForwardSpeedRefresh()
    {
        yield return new WaitForSeconds(0.15f);
        forwardSpeed = 10f;
    }

    private IEnumerator CooldownRefresh()
    {
        yield return new WaitForSeconds(0.3f);
        coolDown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boat hit something!");
        if (other.gameObject.CompareTag("Hazard"))
        {
            damageFlash.StartCoroutine(damageFlash.FlashDamage());
        }
    }
}

