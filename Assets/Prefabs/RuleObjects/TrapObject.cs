using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrapObject : MonoBehaviour, IGameEntity
{
    private GameManager gameManager;

    private void Start()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<EnemyFollowStateChase>(true))
        {
            SendActionToGameManager();
            this.gameObject.GetComponent<Collider>().enabled = true;
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

