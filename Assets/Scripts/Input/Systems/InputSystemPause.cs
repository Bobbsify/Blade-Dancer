using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemPause : MonoBehaviour, IInputEntity
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
    private string pauseAxisName;

    [SerializeField]
    private PauseIconController iconController;

    private bool canPause = true;

    private IInputReceiverPause[] receivers;

    private void OnValidate()
    {
        if (this.searchRoot == null) { 
            InputManager manager;
       
            if (TryGetComponent(out manager))
            {
                this.searchRoot = manager.GetRoot();
            }
        }

        if (iconController == null) 
        {
            iconController = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<PauseIconController>(true);
        }
    }

    private void Start()
    {
        this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverPause>(true);
    }

    private void Update()
    {
        if (this.inputType == InputType.Update)
        {
            this.SendInput();
        }   
    }

    private void FixedUpdate()
    {
        if (this.inputType == InputType.FixedUpdate)
        {
            this.SendInput();
        } 
    }

    private void SendInput()
    {
        if (Input.GetButtonUp(pauseAxisName))
        {
            if (canPause)
            {
                for (int i = 0; i < this.receivers.Length; i++)
                {
                    IInputReceiverPause receiver = this.receivers[i];
                    receiver.ReceiveInputPause();
                }
            }
            else 
            {
                iconController.ExecutePause();
            }
        }
    }

    public void ToggleInput(bool state)
    {
        this.canPause = state;
    }
}