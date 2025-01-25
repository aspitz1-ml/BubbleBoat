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
        if(collider.gameObject.CompareTag("Boat"))
        {
            Debug.Log("Adding fuel to boat");
            Fuel.Instance.AddFuel(10);
        }
    }
}
