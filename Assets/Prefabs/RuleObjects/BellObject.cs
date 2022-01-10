using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BellObject : MonoBehaviour,IGameEntity
{
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<ProjectileController>(true))
        {
            SendActionToGameManager();
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Ring);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
