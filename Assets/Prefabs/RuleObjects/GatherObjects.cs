﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GatherObjects : MonoBehaviour,IGameEntity
{
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<PlayerController>(true))
        {
            SendActionToGameManager();
            Destroy(this.gameObject);
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Gather);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}