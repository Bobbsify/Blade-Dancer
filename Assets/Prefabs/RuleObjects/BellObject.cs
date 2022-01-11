using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BellObject : MonoBehaviour,IGameEntity
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
        if (other.GetComponentInChildren<ProjectileController>() != null)
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
