using UnityEngine;

public class Fuel : MonoBehaviour
{
    public static Fuel Instance { get; private set; } // Singleton instance

    public float maxFuel = 100f; // Maximum fuel level
    public float currentFuel = 0f; // Current fuel level
    public float fuelConsumptionRate = 10f; // How fast fuel is consumed

    private Boat boat; // Reference to the boat script

    private void Awake()
    {
        // Ensure there's only one instance of Fuel
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
        
        Instance = this; // Set the singleton instance
        DontDestroyOnLoad(gameObject); // Keep Fuel across scenes
    }

    public void AddFuel(float amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel); // Clamp fuel to maxFuel
    }
}