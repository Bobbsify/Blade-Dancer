using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseIconController : MonoBehaviour
{
    [SerializeField]
    [Range(2, 6)]
    private int flashes = 2;

    [SerializeField]
    [Range(0.5f, 2.0f)]
    private float flashDuration = 0.8f;

    private Image pauseIcon;

    private bool isInTransition = false;

    private void Awake()
    {
        TryGetComponent(out pauseIcon);
    }

    public void ExecutePause()
    {
        if (!isInTransition) 
        {
            isInTransition = true;
            StartCoroutine(PauseAnimation());
        }
    }

    private IEnumerator PauseAnimation() 
    {
        for (int i = 0; i < flashes; i++) 
        { 
            pauseIcon.enabled = true;
            yield return new WaitForSeconds(flashDuration);
            pauseIcon.enabled = false;
            yield return new WaitForSeconds(flashDuration);
        }
        isInTransition = false;
    }
}
