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
    private GameObject[] hiddenElements;

    [SerializeField]
    private GameObject[] shownElements;

    [SerializeField]
    public GameObject pausedFirstButton;

    [SerializeField]
    public GameObject optionsFirstButton;

    [SerializeField]
    public GameObject optionClosedButton;

    [SerializeField]
    private string pauseBackName;


    private CursorSetter cursorSetter;

    private GameObject player;

    private InputSystemInteract interactManager;

    private IAbility[] abilities;

    private void Start()
    {
        Debug.Log("start");
        cursorSetter = GameObject.FindGameObjectWithTag("UI").GetComponent<CursorSetter>();
        player = GameObject.FindGameObjectWithTag("Player");
        abilities = player.GetComponentsInChildren<IAbility>(true);
        interactManager = GameObject.Find("InputManager").GetComponentInChildren<InputSystemInteract>();
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonUp(pauseBackName))
        {
            BackInputPause();
        }
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

        //Show elements that are supposed to be shown and vice versa
        foreach (GameObject uiElement in hiddenElements)
        {
            uiElement.SetActive(false);
        }
        foreach (GameObject uiElement in shownElements)
        {
            uiElement.SetActive(true);
        }
        //...

        homePage.SetActive(true);
        try
        {
            cursorSetter.SetCursor(CursorType.Menu);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not find cursorSetter");
        }
        Time.timeScale = 0f;
        pause = true;
        EventSystem.current.SetSelectedGameObject(null);
        DisableAllAbilities();
        interactManager.enabled = false;

    }

    public void BackInputPause()
    {
        if (homePage.activeSelf)
        {
            pauseMenu.SetActive(false);
            cursorSetter.SetCursor(CursorType.Game);
            Time.timeScale = 1f;
            pause = false;
            EnableAllAbilities();
            interactManager.enabled = true;
        }
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
        if (abilities.Length > 0)
        { 
            foreach (IAbility ability in abilities)
            {
                ability.Disable();
            }
        }
    }

    public void EnableAllAbilities()
    {
        if (abilities.Length > 0)
        {
            foreach (IAbility ability in abilities)
            {
                ability.Enable();
            }
        }
    }
}
