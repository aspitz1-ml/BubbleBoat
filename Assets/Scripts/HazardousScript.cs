using UnityEngine;

public class HazardousScript: MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hazard hit something!");
        if (other.gameObject.CompareTag("Boat"))
        {
            // Reduce the player's health
            if (Fuel.Instance != null)
            {
                Debug.Log("Hazard hit the boat! Reducing fuel by 10...");
                Fuel.Instance.AddFuel(-10f);
            }
        }
    }
}
