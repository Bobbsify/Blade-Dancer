using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemInteract : MonoBehaviour
{
    [SerializeField]
    private GameObject searchRoot;

    [SerializeField]
    private string interactAxisName;

    private IInputReceiverInteract[] receivers;

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
        this.receivers = this.searchRoot.GetComponentsInChildren<IInputReceiverInteract>(true);
    }

    private void Update()
    {
        if (Input.GetAxisRaw(interactAxisName) > 0f)
        {
            for (int i = 0; i < this.receivers.Length; i++)
            {
                IInputReceiverInteract receiver = this.receivers[i];
                receiver.ReceiveInputInteract();
            }
        }
    }
}
