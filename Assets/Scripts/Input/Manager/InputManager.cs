using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour, IInputManager
{
	[SerializeField]
	private GameObject searchRoot;

	[SerializeField]
	private bool isPlayerUsingController;

	private IInputEntity[] inputSenders;

	private void Start()
	{
		this.inputSenders = GetComponentsInChildren<IInputEntity>(true);
	}
	public GameObject GetRoot()
	{
		return this.searchRoot;
	}

	public void EnablePause()
	{
		if (TryGetComponent(out InputSystemPause pause))
		{
			pause.ToggleInput(true);
		}
	}

	public void DisablePause()
	{
		if (TryGetComponent(out InputSystemPause pause))
		{
			pause.ToggleInput(false);
		}
	}

	public void IsControllerConnected()
	{
		if (Input.GetJoystickNames() != null)
		{
			isPlayerUsingController = true;
		}
		else
		{
			isPlayerUsingController = false;
		}
	}

	void IInputManager.SetupInput(bool enabled)
	{
		this.gameObject.SetActive(enabled);
	}
}