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

    private void Update()
    {
      if(Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button1)|| Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.Joystick1Button9))
		{
			Cursor.lockState = CursorLockMode.Locked;
        }
	  else if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
			Cursor.lockState = CursorLockMode.None;
        }
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

	void IInputManager.SetupInput(bool enabled)
	{
		this.gameObject.SetActive(enabled);
	}
}