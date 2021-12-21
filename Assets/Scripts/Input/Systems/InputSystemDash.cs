using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemDash : MonoBehaviour
{
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
        if (Input.GetAxisRaw(dashAxisName) > 0f)
        {
            for (int i = 0; i < this.receivers.Length; i++)
            {
                IInputReceiverDash receiver = this.receivers[i];
                receiver.ReceiveInputDash();
            }
        }
    }
}
