using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemInteract : MonoBehaviour, IInputEntity
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
            foreach(IInputReceiverInteract receiver in this.searchRoot.GetComponentsInChildren<IInputReceiverInteract>(true))
            {
                receiver.ReceiveInputInteract();
            }
        }
    }
    public void ToggleInput(bool state)
    {
        this.enabled = state;
    }
}
