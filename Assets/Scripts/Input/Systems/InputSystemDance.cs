using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemDance : MonoBehaviour, IInputEntity
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
    private string danceAxisName;

    private IInputReceiverDance[] receivers;

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
        this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverDance>(true);
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
        if (Input.GetButtonUp(danceAxisName))
        {
            for (int i = 0; i < this.receivers.Length; i++)
            {
                IInputReceiverDance receiver = this.receivers[i];
                receiver.ReceiveInputDance();
            }
        }
    }

    public void ToggleInput(bool state)
    {
        this.enabled = state;
    }
}
