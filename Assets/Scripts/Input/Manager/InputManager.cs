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
	}
	public GameObject GetRoot()
	{
		return this.searchRoot;
	}

	public void DisableInput<T>() where T : IInputEntity
	{
		foreach (IInputEntity receiver in receivers)
		{
			if (receiver.GetType() is T) 
			{
				receiver.ToggleInput(false);
			}
		}
	}

	public void EnableInput<T>() where T : IInputEntity
	{
		foreach (IInputEntity receiver in receivers)
		{
			if (receiver.GetType() is T)
			{
				receiver.ToggleInput(true);
			}
		}
	}

	void IInputManager.SetupInput(bool enabled)
	{
		this.gameObject.SetActive(enabled);
	}
}