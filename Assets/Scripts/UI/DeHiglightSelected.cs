using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeHiglightSelected : MonoBehaviour
{
    [SerializeField]
    public GameObject pausedFirstButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            EventSystem.current.SetSelectedGameObject(pausedFirstButton);
        }
    }
}
