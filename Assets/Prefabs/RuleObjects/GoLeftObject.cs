using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GoLeftObject : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInChildren<PlayerController>() != null)
        {
            SendActionToGameManager();
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Left);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
