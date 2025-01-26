using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color originalColor;
    public float flashDuration = .15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public IEnumerator FlashDamage()
    {
        // flash 3x
        for (int i = 0; i < 3; i++)
        {
            meshRenderer.material.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            meshRenderer.material.color = originalColor;
        }

    }
}
