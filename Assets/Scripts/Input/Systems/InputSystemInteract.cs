using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemInteract : MonoBehaviour
{
    private enum InputType
    {
        Update,
        FixedUpdate
    }

    [SerializeField]
    private InputType inputType = InputType.FixedUpdate;

    [SerializeField]
    private GameObject searchRoot;

    [SerializeField]
    private string interactAxisName;

    [SerializeField]
    private bool GetFromInputManager = true;

    private IInputReceiverInteract[] receivers;

    private void OnValidate()
    {
        InputManager manager;
        if (TryGetComponent(out manager) && GetFromInputManager)
        {
            this.searchRoot = manager.GetRoot();
        }
    }

    private void Start()
    {
        this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverInteract>(true);
    }

    private void Update()
    {
        if (this.inputType == InputType.Update)
            this.SendInput();
    }

    private void FixedUpdate()
    {
        if (this.inputType == InputType.FixedUpdate)
            this.SendInput();
    }

    private void SendInput()
    {
        if (Input.GetButtonUp(interactAxisName))
        {
            for (int i = 0; i < this.receivers.Length; i++)
            {
                IInputReceiverInteract receiver = this.receivers[i];
                receiver.ReceiveInputInteract();
            }
        }
    }
}
