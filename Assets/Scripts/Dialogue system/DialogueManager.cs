using System.Collections;
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
    private GameObject UIdialogue;

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

    public void ReceiveInputInteract()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerDistance = player.transform.position - transform.position;
        if (playerDistance.magnitude <= distanceToTrigger)
        {
            if (isInDialogue)
            {
                UIdialogue.SetActive(true);
                player.GetComponent<PlayerController>().DisableAllAbilities();
                dialogueUI.SetDialogue(startingDialogue);
                nextDialogue = startingDialogue.GetNextDialogue();
            }
            else
            {
                isInDialogue = true;
                dialogueUI.SetDialogue(startingDialogue);
                nextDialogue = startingDialogue.GetNextDialogue();
            }

            if (nextDialogue == null)
            {
                isInDialogue = false;
                dialogueUI.EndDialogue();
                UIdialogue.SetActive(false);
                player.GetComponent<PlayerController>().EnableAllAbilities();
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
