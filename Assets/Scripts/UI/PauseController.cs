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
    public GameObject pausedFirstButton;

    [SerializeField]
    public GameObject optionsFirstButton;

    [SerializeField]
    public GameObject optionClosedButton;

    private CursorSetter cursorSetter;

    private void Start()
    {
        cursorSetter = GameObject.FindGameObjectWithTag("UI").GetComponent<CursorSetter>();
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
        cursorSetter.SetCursor(CursorType.Game);
        Time.timeScale = 1f;
        pause = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        cursorSetter.SetCursor(CursorType.Menu);
        Time.timeScale = 0f;
        pause = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pausedFirstButton);

    }

    public void SettingOption()
    {
        settingMenu.SetActive(true);
        homePage.SetActive(false);
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
}
