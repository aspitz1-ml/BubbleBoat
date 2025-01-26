using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnGameOver()
    {

        gameObject.SetActive(true);
    }

    // public void OnGameRestart()
    // {
    //     gameOverText.SetActive(false);
    // }
}
