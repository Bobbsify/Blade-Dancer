using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private Text timerText;

    private GameManager gameManager;

    private float maxTime;

    private float currentTime;

    private bool doTimer = false;

    // Update is called once per frame
    void Update()
    {
        if (doTimer) {         
            currentTime -= Time.deltaTime/2;
            float truncatedTime = (float)Math.Round((currentTime) * 100f) / 100f;
            timerText.text = truncatedTime.ToString().Replace(',', ':');
            if (currentTime <= 0) 
            {
                doTimer = false;
                gameManager.KillPlayer();
            }
        }
    }

    public void SetTimer(float amount) 
    {
        maxTime = amount;
        currentTime = amount;
        doTimer = true;
    }

    public void StopTimer()
    {
        doTimer = false;
    }

    public void StartTimer()
    {
        doTimer = true;
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
