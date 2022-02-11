using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonJoystickMC : MonoBehaviour
{
    [SerializeField]
    public GameObject ButtonToshow;

    // Update is called once per frame
    void Update()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ButtonToshow);
    }
}
