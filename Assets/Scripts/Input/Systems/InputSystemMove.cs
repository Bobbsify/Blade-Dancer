using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemMove : MonoBehaviour
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
	private string horizontalAxisName = "Horizontal";

	[SerializeField]
	private string verticalAxisName = "Vertical";

	private IInputReceiverMove[] receivers;

    private void OnValidate()
    {
        InputManager manager;
        if(TryGetComponent(out manager))
        {
            this.searchRoot = manager.GetRoot();
        }
    }

    private void Start()
	{
		this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverMove>(true);
	}

	private void Update()
	{
		if (this.inputType == InputType.Update)
			this.SendInput();
	}

	private void FixedUpdate()
	{
		if(this.inputType == InputType.FixedUpdate)
			this.SendInput();
	}

	private void SendInput()
	{
		float horizontal = Input.GetAxisRaw(this.horizontalAxisName);   // -1f, 0f, 1f
		float vertical = Input.GetAxisRaw(this.verticalAxisName);      // -1f, 0f, 1f

		Vector3 dir = new Vector3(horizontal, 0f, vertical);
		dir.Normalize();

		for (int i = 0; i < this.receivers.Length; i++)
		{
			IInputReceiverMove receiver = this.receivers[i];
			receiver.ReceiveInputMove(dir);
		}
	}
}