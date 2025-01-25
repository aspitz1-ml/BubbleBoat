using UnityEngine;
using UnityEngine.UI;

public class FuelMeter : MonoBehaviour
{
    public Image fuelMeterFill;
    public float maxFuel = 100f;
    public float currentFuel;

    void Start()
    {
        currentFuel = maxFuel;  // Set initial fuel
    }

    void Update()
    {
        if (Fuel.Instance != null)
        {

            currentFuel = Fuel.Instance.currentFuel;
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