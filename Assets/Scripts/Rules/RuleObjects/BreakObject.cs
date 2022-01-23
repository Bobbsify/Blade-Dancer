using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BreakObject : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private float objectHealth;

    [SerializeField]
    private float healthToDestroyGameObject;

    private void Start()
    {
        objectHealth = objectHealth * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<ProjectileController>() != null)
        {
            objectHealth--;
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
