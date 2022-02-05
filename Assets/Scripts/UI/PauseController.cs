using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour, IInputReceiverPause
{
    [SerializeField]
    private static bool pause;
    [SerializeField]
    private GameObject pauseMenu;

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
