using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class BreakObject : MonoBehaviour, IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private float objectHealth;

    [SerializeField]
    private SoundPacket damageSound;

    [SerializeField]
    private SoundPacket breakSound;

    private Animator animator;

    private float maxHealth;
    private bool damaged = false;

    private void Start()
    {
        TryGetComponent(out animator);
        objectHealth = objectHealth * 2;
        maxHealth = objectHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<ProjectileController>() != null && this.enabled)
        {
            DestroyIfZeroHealth();
        }
    }

    private void DestroyIfZeroHealth()
    {
        objectHealth--;
        Debug.Log(this.objectHealth + " == " + maxHealth + " / 2" + (this.objectHealth == maxHealth / 2));
        if (this.objectHealth == maxHealth / 2)
        {
            SendActionToGameManager();
            gameManager.PlaySound(damageSound);
            animator.SetTrigger("damage");
            damaged = true;
        }
        else if (this.objectHealth == 0) //no health
        {
            gameManager.PlaySound(breakSound);
            animator.SetTrigger("damage");
            this.enabled = false;
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
