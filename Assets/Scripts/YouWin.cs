using UnityEngine;

public class YouWin : MonoBehaviour
{
    public GameObject restartButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
               restartButton = GameObject.Find("RestartButton");

        if (restartButton != null)
        {
            restartButton.SetActive(false);
        } 
    }

    public void OnYouWin()
    {
        gameObject.SetActive(true);
        restartButton.SetActive(true);

    }
}
    

