using UnityEngine;

public class FinishLine : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
            Debug.Log("Player reached the finish line!");
        }
    }
}
