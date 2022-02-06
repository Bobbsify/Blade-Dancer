﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDanceController : MonoBehaviour
{
    [SerializeField]
    public List<Image> chargeBackgrounds = new List<Image>();

    private Color whiteColor = new Color(0.95311f, 0.95311f, 0.95311f);
    private Color purpleColor = new Color(0.56078f, 0.35294f, 0.85882f);

    private int maxCharge;

    private void OnValidate()
    {
        if (chargeBackgrounds.Count == 0) 
        {
            foreach (Transform t in transform) 
            {
                chargeBackgrounds.Add(t.GetComponent<Image>());
            }
        }
    }

    private void Start()
    {
        maxCharge = chargeBackgrounds.Count;
    }

    public void UpdateCharge(int charge) 
    {
        for (int i = charge; i < maxCharge; i++)
        {
            chargeBackgrounds[i].color = whiteColor;
        }
        for (int i = 0; i < charge; i++)
        {
            chargeBackgrounds[i].color = purpleColor;
        }
    }
}