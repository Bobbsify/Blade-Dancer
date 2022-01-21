using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemDash : MonoBehaviour, IInputEntity
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
    private string dashAxisName;

    private IInputReceiverDash[] receivers;

    private void OnValidate()
    {
        InputManager manager;
        if (TryGetComponent(out manager))
        {
            this.searchRoot = manager.GetRoot();
        }
    }

    private void Start()
    {
        this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverDash>(true);
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
       
        if (Input.GetAxisRaw(this.dashAxisName) > 0f)
        {
            for (int i = 0; i < this.receivers.Length; i++)
            {
                IInputReceiverDash receiver = this.receivers[i];
                receiver.ReceiveInputDash();
            }
        }
    }

    public void ToggleInput(bool state)
    {
        this.enabled = state;
    }
}
