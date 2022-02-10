using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BellObject : MonoBehaviour,IGameEntity
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private SoundPacket sound;

    private SoundQueueManager sqm;

    [SerializeField]
    private bool fadeIn;

    private void Awake()
    {
        if (TryGetComponent(out Collider col))
        {
            col.isTrigger = true;
        }
    }

    private void Start()
    {
        this.sqm = gameManager.GetComponentInChildren<SoundQueueManager>();
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
        gameManager.PlaySound(this.sound, fadeIn);
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
