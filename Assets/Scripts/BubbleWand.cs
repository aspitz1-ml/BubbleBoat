using UnityEngine;

public class BubbleWand : MonoBehaviour
{

    public float fuelAmount = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }


    // Update is called once per frame
    void Update()
    {

        
    }
    

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Collided with: " + collider.gameObject.name);
        if(collider.gameObject.CompareTag("Boat") && Fuel.Instance != null)
        {
            Debug.Log("Bubble Wand! Adding fuel to boat");
            Fuel.Instance.AddFuel(fuelAmount);
        } 

        if (Fuel.Instance == null)
        {
            Debug.Log("Fuel instance not found");
        }
    }
}
