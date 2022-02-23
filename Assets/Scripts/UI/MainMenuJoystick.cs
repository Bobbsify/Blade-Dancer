using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuJoystick : MonoBehaviour
{
    [SerializeField]
    public GameObject firstButton;

    [SerializeField]
    public GameObject secondButton;

    [SerializeField]
    public GameObject MusicButton;

    [SerializeField]
    public GameObject settingPage;

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
        EventSystem.current.SetSelectedGameObject(firstButton);
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
            homePage.SetActive(true);
            settingPage.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        if (controls.activeSelf)
        {
            settingPage.SetActive(true);
            controls.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondButton);
        }
    }

    public void GotoSettings()
    {
        settingPage.SetActive(true);
        MusicButton.SetActive(false);
        homePage.SetActive(false);
        MusicButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(secondButton);
    }

    public void GotoBackToMenu()
    {
        homePage.SetActive(true);
        settingPage.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
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
    }

    public void GoBackToSettings()
    {
        settingPage.SetActive(true);
        controls.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(secondButton);
    }

}
