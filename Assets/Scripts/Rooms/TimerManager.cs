using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private List<Slider> timer;

    [Header("Cheer")]
    [SerializeField]
    [Tooltip("Percentage of timer needed for a cheer")]
    [Range(1, 90)]
    private float cheerThreshold = 50;

    [SerializeField]
    private int cheerChargingBonus = 2;

    [Header("Sound")]
    [SerializeField]
    private SoundPacket timesUpSound;

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
            foreach (Slider slider in timer) 
            {
                slider.value = truncatedTime;
            }
            if (currentTime <= 0) 
            {
                doTimer = false;
                gameManager.PlaySound(timesUpSound);
                gameManager.KillPlayer();
            }
        }
    }

    public void SetTimer(float amount)
    {
        maxTime = amount;
        currentTime = amount;
        float truncatedTime = (float)Math.Round((currentTime) * 100f) / 100f;
        foreach (Slider slider in timer) 
        {
            slider.maxValue = amount;
            slider.value = amount;
            slider.minValue = 0;
        }
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
        foreach (Slider slider in timer) 
        {
            slider.value = slider.maxValue;
        }
    }
    public int GetCheer()
    {
        return currentTime > maxTime * cheerThreshold / 100 ? cheerChargingBonus : 1;
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public bool IsGoing()
    {
        return doTimer;
    }
}
