using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlagObject : MonoBehaviour,IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<PlayerController>() != null)
        {
            SendActionToGameManager();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInChildren<FSM>() != null)
        {
            gameManager.KillPlayer();
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
