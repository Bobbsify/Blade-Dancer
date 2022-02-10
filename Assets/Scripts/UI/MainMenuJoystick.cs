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
    public GameObject thirdButton;

    [SerializeField]
    public GameObject MusicButton;

    [SerializeField]
    public GameObject settingPage;

    [SerializeField]
    public GameObject homePage;

    [SerializeField]
    public GameObject controls;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(thirdButton);
    }

    public void GoBackToSettings()
    {
        settingPage.SetActive(true);
        controls.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(secondButton);
    }

}
