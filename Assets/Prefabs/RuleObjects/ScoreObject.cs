using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScoreObject : MonoBehaviour
{
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<BallObject>(true))
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
