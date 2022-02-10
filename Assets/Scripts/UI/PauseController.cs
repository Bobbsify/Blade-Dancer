using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseController : MonoBehaviour, IInputReceiverPause
{
    [SerializeField]
    private static bool pause;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject homePage;

    [SerializeField]
    private GameObject settingMenu;

    [SerializeField]
    private GameObject controls;

    [SerializeField]
    private GameObject musics;

    [SerializeField]
    public GameObject pausedFirstButton;

    [SerializeField]
    public GameObject optionsFirstButton;

    [SerializeField]
    public GameObject optionClosedButton;

    [SerializeField]
    public GameObject controlsButton;

    private CursorSetter cursorSetter;

    private GameObject player;

    private InputSystemInteract interactManager;

    private IAbility[] abilities;

    private void Start()
    {
        cursorSetter = GameObject.FindGameObjectWithTag("UI").GetComponent<CursorSetter>();
        player = GameObject.FindGameObjectWithTag("Player");
        abilities = player.GetComponentsInChildren<IAbility>(true);
        interactManager = GameObject.Find("InputManager").GetComponentInChildren<InputSystemInteract>();
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
        cursorSetter.SetCursor(CursorType.Game);
        Time.timeScale = 1f;
        pause = false;
        EnableAllAbilities();
        interactManager.enabled = true;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        controls.SetActive(false);
        settingMenu.SetActive(false);
        homePage.SetActive(true);
        cursorSetter.SetCursor(CursorType.Menu);
        Time.timeScale = 0f;
        pause = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pausedFirstButton);
        DisableAllAbilities();
        interactManager.enabled = false;

    }

    public void SettingOption()
    {
        settingMenu.SetActive(true);
        musics.SetActive(false);
        homePage.SetActive(false);
        musics.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void BackFromSettings()
    {
        homePage.SetActive(true);
        settingMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionClosedButton);
    }

    public void BackFromControls()
    { 
        settingMenu.SetActive(true);
        controls.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void GoToControls()
    {
        settingMenu.SetActive(false);
        controls.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsButton);
    }

    public void ReceiveInputPause()
    {
        if (pause)
        {
            Continue();
        }
        else 
        {
            Pause();
        }
    }

    public void DisableAllAbilities()
    {
        foreach (IAbility ability in abilities)
        {
            ability.Disable();
        }
    }

    public void EnableAllAbilities()
    {
        foreach (IAbility ability in abilities)
        {
            ability.Enable();
        }
    }
}
