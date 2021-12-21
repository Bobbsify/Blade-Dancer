using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemShoot : MonoBehaviour
{
	[SerializeField]
	private GameObject searchRoot;

	[SerializeField]
	private string axisName = "Fire2";

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
		if (Input.GetAxisRaw(this.axisName) > 0f)
		{
			for (int i = 0; i < this.receivers.Length; i++)
			{
				IInputReceiverShoot receiver = this.receivers[i];
				receiver.ReceiveInputShoot();
			}
		}
	}
}