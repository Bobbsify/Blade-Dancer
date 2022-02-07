using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonAction : MonoBehaviour
{
    [SerializeField]
    private string commandAxis;

    [SerializeField]
    private UnityEvent actions;

    private void Update()
    {
        if (Input.GetButtonUp(commandAxis)) 
        {
            actions.Invoke();
        }
    }
}
