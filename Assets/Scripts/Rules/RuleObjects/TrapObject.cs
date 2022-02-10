using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TrapObject : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject trapCollider;

    [SerializeField]
    private SoundPacket trappingSound;

    private bool canBeTrapped;

    private void Start()
    {
        trapCollider.GetComponent<Collider>().enabled = false;
        canBeTrapped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<FSM>() != null)
        {
            Destroy(other.GetComponent<EnemyChase>());
            trapCollider.GetComponent<Collider>().enabled = true;

            if(canBeTrapped == true)
            {
                gameManager.PlaySound(trappingSound);
                SendActionToGameManager();
                canBeTrapped = false;
            }
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

