using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject restartButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);

        if (restartButton != null)
        {
            Debug.Log("Restart button found");
            restartButton.SetActive(false);
        }
    }

    public void OnGameOver()
    {
        gameObject.SetActive(true);
        restartButton.SetActive(true);
    }
}
