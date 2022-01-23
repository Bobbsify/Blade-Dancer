using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScoreObject : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        if (TryGetComponent(out Collider col))
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<BallObject>() != null)
        {
            SendActionToGameManager();
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Score);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
