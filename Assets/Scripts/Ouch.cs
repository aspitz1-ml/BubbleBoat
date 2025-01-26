using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouch : MonoBehaviour
{
    public bool isOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }


    public void SetVisible(bool active)
    {
        gameObject.SetActive(active);
    }

    // public IEnumerator FlashDamage()
    // {
    //     // flash 3x
    //     Debug.Log("Ouch!");
    //     gameObject.SetActive(true);
    //     yield return new WaitForSeconds(flashDuration);
    //     gameObject.SetActive(false);
    //     yield return new WaitForSeconds(flashDuration);
    // }
}
