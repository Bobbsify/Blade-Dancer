using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour, IInputReceiverPause
{
    [SerializeField]
    private static bool pause;
    [SerializeField]
    private GameObject pauseMenu;

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
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
