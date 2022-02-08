using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeHiglightSelected : MonoBehaviour
{
    [SerializeField]
    public GameObject ButtonToshow;

    public bool DeHilight;

    public void OnPointerEnter()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPointerExit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ButtonToshow);
    }

   /* private void Start()
    {
        DeHilight = true;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
        {
            if(DeHilight == true)
            {
                EventSystem.current.SetSelectedGameObject(null);
                DeHilight = false;
            }
        }

        else if (Input.GetAxisRaw("Mouse X") == 0 && Input.GetAxisRaw("Mouse Y") == 0)
        {
            if (DeHilight == false)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(ButtonToshow);
                DeHilight = true;
            }
        } 
    }*/
}
