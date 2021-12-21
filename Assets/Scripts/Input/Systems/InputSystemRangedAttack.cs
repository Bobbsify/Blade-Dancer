using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystemRangedAttack : MonoBehaviour
{
	[SerializeField]
	private GameObject searchRoot;

	[SerializeField]
	private string axisName = "Fire2";

	private IInputReceiverRangedAttack[] receivers;

	private void Start()
	{
		this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverRangedAttack>(true);
	}

	private void Update()
	{
		if (Input.GetAxisRaw(this.axisName) > 0f)
		{
			for (int i = 0; i < this.receivers.Length; i++)
			{
				IInputReceiverRangedAttack receiver = this.receivers[i];
				receiver.ReceiveInputRangedAttack();
			}
		}
	}
}