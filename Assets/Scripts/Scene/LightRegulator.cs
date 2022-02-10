using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightRegulator : MonoBehaviour
{
    [SerializeField]
    private Color[] colors;

    [SerializeField]
    [Range(0.1f,10f)]
    private float lightDelay = 0.5f;

    private Light thisLight;

    private int currentColor = 1;

    private void Awake()
    {
        TryGetComponent(out thisLight);
        if (colors.Length > 0) 
        {
            StartCoroutine(DoLights());
        }
    }

    private IEnumerator DoLights() 
    {
        yield return new WaitForSeconds(lightDelay);
        thisLight.color = colors[currentColor];
        currentColor++;
        if (currentColor == colors.Length) { currentColor = 0; }
        StartCoroutine(DoLights());
    }
}
