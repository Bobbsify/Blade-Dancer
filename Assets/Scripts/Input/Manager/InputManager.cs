using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour, IInputManager
{
	[SerializeField]
	private GameObject searchRoot;

	private IInputEntity[] inputSenders;

	private void Start()
	{
		this.inputSenders = GetComponentsInChildren<IInputEntity>(true);
	}

    private void Update()
	{ 
		if(CheckJoystickInput())
		{
			Cursor.lockState = CursorLockMode.Locked;
        }

		else if (CheckKeyBoardInput())
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

	private bool CheckJoystickInput()
    {
		return Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button8) || Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetAxisRaw("JoystickAxis6") != 0 || Input.GetAxisRaw("JoystickAxis7") != 0 || Input.GetAxisRaw("JoystickAxis9") != 0 || Input.GetAxisRaw("JoystickAxis10") != 0;
	}

	private bool CheckKeyBoardInput()
    {
		return Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape) || Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0 || Input.GetAxisRaw("TabInputManager") != 0;
	}
}