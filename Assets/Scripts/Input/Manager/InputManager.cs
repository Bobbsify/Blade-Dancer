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
      if(Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button1)|| Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick1Button8) || Input.GetKeyDown(KeyCode.Joystick1Button9))
		{
			Debug.Log("cursorLock");
			Cursor.lockState = CursorLockMode.Locked;
        }
	  else if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape))
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