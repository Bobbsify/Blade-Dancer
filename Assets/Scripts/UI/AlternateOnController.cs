﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateOnController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> onControllerOn;
    [SerializeField]
    private List<GameObject> onControllerOff;

    private void Awake()
    {
        ControllerOff();
    }

    private void Update()
    {
        if (Input.GetJoystickNames().Length == 0)
        {
            ControllerOff();
        }
        else 
        {
            ControllerOn();
        }
    }

    private void ControllerOn()
    {
        foreach (GameObject item in onControllerOff)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in onControllerOn)
        {
            item.SetActive(true);
        }
    }

    private void ControllerOff()
    {
        foreach (GameObject item in onControllerOff)
        {
            item.SetActive(true);
        }
        foreach (GameObject item in onControllerOn)
        {
            item.SetActive(false);
        }
    }
}
