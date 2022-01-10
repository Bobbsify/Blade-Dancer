using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BreakObject : MonoBehaviour, IGameEntity
{
    private GameManager gameManager;

    [SerializeField]
    private float objectHealth;

    [SerializeField]
    private float healthToDestroyGameObject;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<ProjectileController>(true))
        {
            this.objectHealth--;
            DestroyIfZeroHealth();
        }
    }

    private void DestroyIfZeroHealth()
    {
        if (this.objectHealth <= healthToDestroyGameObject)
        {
            SendActionToGameManager();
            Destroy(this.gameObject);
        }      
    }

    public void SendActionToGameManager()
    {
        this.gameManager.ActionEventTrigger(Actions.Break);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
