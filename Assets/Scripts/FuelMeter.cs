using UnityEngine;
using UnityEngine.UI;

public class FuelMeter : MonoBehaviour
{
    public Image fuelMeterFill;  // Drag the HealthBarFill image here in the Inspector
    public float maxFuel = 100f;  // Set max health
    private float currentFuel;

    void Start()
    {
        Debug.Log("FuelMeter script started");
        currentFuel = maxFuel;  // Set initial health
    }

    void Update()
    {
        // For testing, decrease health over time (you can remove this part in the final version)
        if (Input.GetKey(KeyCode.Space))  // Press space to decrease health
        {

            Debug.Log("Removing fuel...");
            currentFuel -= 1f * Time.deltaTime * 10f;  // Decrease health slowly for testing
        }

        // Clamp the current health to ensure it doesn't go below 0 or above max
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);

        // Update the health bar fill based on current health
        UpdateFuelMeter();
    }

    void UpdateFuelMeter()
    {
        if (fuelMeterFill != null)
        {
            fuelMeterFill.fillAmount = currentFuel / maxFuel;  // Set the fill amount
        }
    }

    // Optionally, you can create a public method to set health from other scripts
    public void SetFuel(float fuel)
    {
        currentFuel = fuel;
        UpdateFuelMeter();
    }
}