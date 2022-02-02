﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour, IInputReceiverInteract
{
    [SerializeField]
    private UIDialogueController dialogueUI;

    [Header("Dialogue")]
    [SerializeField]
    [Tooltip("Distance at which the dialogue starts")]
    private float distanceToTrigger = 5.0f;

    [SerializeField]
    private Dialogue startingDialogue;

    [SerializeField]
    private List<Dialogue> dialogues = new List<Dialogue>();

    [SerializeField]
    UnityEvent endOfDialogueEvents;

    private GameObject player;

    private Dialogue nextDialogue;

    private bool isInDialogue = false;

    private void OnValidate()
    {
        if (dialogues.Count == 0)
        {
            dialogues.AddRange(GetComponentsInChildren<Dialogue>());
        }
        else if(startingDialogue == null)
        {
            startingDialogue = dialogues[0];
        }

    }

    private void Start()
    {
        dialogueUI = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<UIDialogueController>(true);
    }

    public void ReceiveInputInteract()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerDistance = player.transform.position - transform.position;
        if (playerDistance.magnitude <= distanceToTrigger)
        {

            if (isInDialogue)
            {

                if (nextDialogue == null)
                {
                    isInDialogue = false;
                    dialogueUI.EndDialogue(endOfDialogueEvents);
                    player.GetComponent<PlayerController>().EnableAllAbilities();
                    return;
                }

                dialogueUI.SetDialogue(nextDialogue);
                nextDialogue = nextDialogue.GetNextDialogue();
            }
            else
            {
                player.GetComponent<PlayerController>().DisableAllAbilities();
                isInDialogue = true;
                dialogueUI.SetDialogue(startingDialogue);
                nextDialogue = startingDialogue.GetNextDialogue();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color GizmosColor = Color.blue;
        GizmosColor.a = 0.5f;
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(this.transform.position, new Vector3(distanceToTrigger, distanceToTrigger, distanceToTrigger));
    }

    /*TODO | Remove se alla fine non si usa il Game Manager*/
}
