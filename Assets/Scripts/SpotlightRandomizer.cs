using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpotlightRandomizer : MonoBehaviour
{
    [SerializeField]
    private float maxAlpha = 0.7f;

    [Header("Light popup duration")]

    [SerializeField]
    [Range(0.1f,1.0f)]
    private float minPopupTime = 0.0f;

    [SerializeField]
    [Range(0.1f, 1.0f)]
    private float maxPopupTime = 1.0f;

    [Header("Light duration")]

    [SerializeField]
    [Range(1.0f, 1.9f)]
    private float minLightDuration = 2.0f;

    [SerializeField]
    [Range(2.0f, 3.0f)]
    private float maxLightDuration = 2.0f;

    private Image spotlight;
    private Color originalSpotlightColor;

    private float popupMod = 1;
    private int modifier = 1;
    private bool doLight = false;

    private void OnValidate()
    {
        if (minPopupTime > maxPopupTime)
        {
            float a = minPopupTime;
            minPopupTime = maxPopupTime;
            maxPopupTime = a;
        }
    }

    private void Awake()
    {
        TryGetComponent(out spotlight);
        originalSpotlightColor = spotlight.color;
    }

    private void OnEnable()
    {
        DetermineBehaviour();
    }

    private void Update()
    {
        if (doLight) 
        {
            originalSpotlightColor.a += modifier * (popupMod * Time.deltaTime);
            spotlight.color = originalSpotlightColor;
            switch (modifier) 
            {
                case 1:
                    if (originalSpotlightColor.a >= maxAlpha)
                    {
                        originalSpotlightColor.a = maxAlpha;
                        doLight = false;
                    }
                    break;
                case -1:
                    if (originalSpotlightColor.a <= 0)
                    {
                        originalSpotlightColor.a = 0;
                        doLight = false;
                    }
                    break;
                default:
                    throw new System.Exception("Impossible modifier value for " + this);
            }
        }
        
    }

    private void DetermineBehaviour() 
    {
        StartCoroutine(StartLighting(Random.Range(minLightDuration, maxLightDuration)));
    }

    private IEnumerator StartLighting(float lightDuration) 
    {
        modifier = 1;
        popupMod = Random.Range(minPopupTime, maxPopupTime);
        doLight = true;
        yield return new WaitForSeconds(lightDuration);
        modifier = -1;
        doLight = true;
        yield return new WaitForSeconds(lightDuration);
        DetermineBehaviour();
    }
}
