using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour, IGameEntity
{
    private GameManager gameManager;

    private float maxTime;

    private float currentTime;

    private bool doTimer = false;

    // Update is called once per frame
    void Update()
    {
        if (doTimer) {         
            currentTime -= Time.deltaTime;
            Debug.Log(currentTime);
            if (currentTime < 0) 
            {
                doTimer = false;
                gameManager.KillPlayer();
            }
        }
    }

    public void SetTimer(float amount) 
    {
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
