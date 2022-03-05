using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuJoystick : MonoBehaviour
{
    [SerializeField]
    public GameObject mainMenuFirstButton;

    [SerializeField]
    public GameObject settingsFirstButton;

    [SerializeField]
    public GameObject controlsNewGameFirstButton;

    [SerializeField]
    public GameObject MusicButton;

    [SerializeField]
    public GameObject settingPage;

    [SerializeField]
    public GameObject creditsPage;

    [SerializeField]
    public GameObject homePage;

    [SerializeField]
    public GameObject controls;

    [SerializeField]
    public GameObject controlsNewGame;

    [SerializeField]
    private string pauseBackName;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);

        foreach (VolumeManager volume in GetComponentsInChildren<VolumeManager>()) 
        {
            volume.SetupVolume();
        }
    }

    private void Update()
    {
        if (Input.GetButtonUp(pauseBackName))
        {
            BackInputPause();
        }
    }

    public void BackInputPause()
    {
        if (settingPage.activeSelf)
        {
            GoBackToMenu();
        }

        if (controls.activeSelf)
        {
            GoBackToSettings();
        }

        if (controlsNewGame.activeSelf)
        {
            GoBackToMenu();
        }

        if (creditsPage.activeSelf)
        {
            GoBackToMain();
        }
    }

    public void GotoSettings()
    {
        settingPage.SetActive(true);
        MusicButton.SetActive(false);
        homePage.SetActive(false);
        MusicButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void GotoCredits()
    {
        creditsPage.SetActive(true);
        settingPage.SetActive(false);
        homePage.SetActive(false);
    }

    public void GoBackToMain()
    {
        creditsPage.SetActive(false);
        settingPage.SetActive(false);
        homePage.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void GoBackToMenu()
    {
        homePage.SetActive(true);
        settingPage.SetActive(false);
        controlsNewGame.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void GoToControls()
    {
        controls.SetActive(true);
        settingPage.SetActive(false);
    }

    public void GoToControlsNewGame()
    {
        controlsNewGame.SetActive(true);
        homePage.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsNewGameFirstButton);
    }

    public void GoBackToSettings()
    {
        settingPage.SetActive(true);
        controls.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

}
