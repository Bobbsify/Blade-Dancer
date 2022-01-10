using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlagObject : MonoBehaviour,IGameEntity
{
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<PlayerController>(true))
        {
            SendActionToGameManager();
            Destroy(this.gameObject);
        }

        else if (collision.collider.GetComponentInChildren<EnemyFollowStateChase>(true))
        {
            Destroy(this.gameObject);
        }
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Reach);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public Vector3 GetFlagPosition()
    {
        return this.gameObject.transform.position;
    }
}
