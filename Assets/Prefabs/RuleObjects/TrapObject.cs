﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrapObject : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject trapCollider;

    private void Start()
    {
        trapCollider.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<EnemyFollowStateChase>() != null)
        {
            SendActionToGameManager();
            trapCollider.GetComponent<Collider>().enabled = true;
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Trap);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}

