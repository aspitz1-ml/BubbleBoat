using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController Controller;
    private Vector3 Velocity;
    private bool Cooldown;
    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Velocity.y += -15 * Time.deltaTime;

        if(Input.GetKey("space") && Cooldown == false) {
            Cooldown = true;
            Velocity.y = Mathf.Sqrt(60);
            StartCoroutine(CooldownRefresh());
        }
    }

    private IEnumerator CooldownRefresh() {
        yield return new WaitForSeconds(0.3f);
        Cooldown = false;
    }
}
