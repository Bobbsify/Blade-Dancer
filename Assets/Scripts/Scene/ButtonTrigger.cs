using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour, IInputReceiverInteract, IGameEntity
{
    [SerializeField]
    private float distanceToTrigger = 5.0f;

    [SerializeField]
    private bool startStreak = false;

    [SerializeField]
    private UnityEvent otherButtonEvents;

    private GameObject player;

    private GameManager gameManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ReceiveInputInteract()
    {
            Vector3 playerDistance = player.transform.position - this.transform.position;
            if (playerDistance.magnitude <= distanceToTrigger) 
            {
                if (startStreak) 
                {
                    gameManager.StartStreak();
                }
                otherButtonEvents.Invoke();
            }
    }

    private void OnDrawGizmosSelected()
    {
        Color GizmosColor = Color.red;
        GizmosColor.a = 0.5f;
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(this.transform.position, new Vector3(distanceToTrigger, distanceToTrigger, distanceToTrigger));
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
