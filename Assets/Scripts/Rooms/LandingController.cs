using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LandingController : MonoBehaviour, IGameEntity
{
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController pc)) 
        {
            pc.EnableAllAbilities();
            this.enabled = false;
            gameManager.StartStage();
        }
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
