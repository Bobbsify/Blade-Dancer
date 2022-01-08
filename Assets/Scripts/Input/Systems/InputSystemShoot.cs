using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemShoot : MonoBehaviour
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
	private string axisName;

	private IInputReceiverShoot[] receivers;

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
		this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverShoot>(true);
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
		if (Input.GetButtonUp(this.axisName))
		{
			for (int i = 0; i < this.receivers.Length; i++)
			{
				IInputReceiverShoot receiver = this.receivers[i];
				receiver.ReceiveInputShoot();
			}
		}
	}
}