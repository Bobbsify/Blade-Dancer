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
            currentTime -= Time.deltaTime;
            float truncatedTime = (float)Math.Round((currentTime) * 100f) / 100f;
            string textToAdd = truncatedTime.ToString().Replace(',', ':');
            if (textToAdd.Length == 3) textToAdd += "0";
            if (textToAdd.Length == 1) textToAdd += ":00";
            timerText.text = textToAdd;
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
        float truncatedTime = (float)Math.Round((currentTime) * 100f) / 100f;
        string textToAdd = truncatedTime.ToString().Replace(',', ':');
        if (textToAdd.Length == 3) textToAdd += "0";
        if (textToAdd.Length == 1) textToAdd += ":00";
        timerText.text = textToAdd;
    }

    public void StopTimer()
    {
        doTimer = false;
    }

    public void StartTimer()
    {
        doTimer = true;
    }

    public void ResetTimer()
    {
        doTimer = false;
        currentTime = 0;
        timerText.text = "00:00";
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
