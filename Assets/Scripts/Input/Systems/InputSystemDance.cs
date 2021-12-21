using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class InputSystemDance : MonoBehaviour
{
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
        if (Input.GetAxisRaw(danceAxisName) > 0f)
        {
            for (int i = 0; i < this.receivers.Length; i++)
            {
                IInputReceiverDance receiver = this.receivers[i];
                receiver.ReceiveInputDance();
            }
        }
    }
}
