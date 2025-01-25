using UnityEngine;

public class BubbleWand : MonoBehaviour
{

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
            Fuel.Instance.AddFuel(10);
        } 

        if (Fuel.Instance == null)
        {
            Debug.Log("Fuel instance not found");
        }


       
    }
}
