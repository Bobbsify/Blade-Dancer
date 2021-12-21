using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour, IInputManager
{
	[SerializeField]
	private GameObject searchRoot;

	private IInputEntity[] receivers;

	private void Start()
	{
		this.receivers = this.searchRoot.GetComponentsInChildren<IInputEntity>(true);
		
		for (int i = 0; i < this.receivers.Length; i++)
		{
			var receiver = this.receivers[i];
			receiver.InitInput(this);
		}
	}

	void IInputManager.SetupInput(bool enabled)
	{
		this.gameObject.SetActive(enabled);
	}
}