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
    private AudioClip voice;

    [SerializeField]
    private Dialogue startingDialogue;

    [SerializeField]
    private List<Dialogue> dialogues = new List<Dialogue>();

    [Header("Events")]
    [SerializeField]
    UnityEvent startOfDialogueEvents;

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

    private void Awake()
    {
        dialogueUI = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<UIDialogueController>(true);
    }

    public void ReceiveInputInteract()
    {
        if (this.isActiveAndEnabled) { 
            player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerDistance = player.transform.position - transform.position;
            if (playerDistance.magnitude <= distanceToTrigger / 2)
            {
                    if (isInDialogue)
                    {

                        if (dialogueUI.IsTelling()) 
                        {
                            dialogueUI.FillDialogue();
                        }
                        else
                        {
                            if (nextDialogue == null)
                            {
                                isInDialogue = false;
                                dialogueUI.EndDialogue(endOfDialogueEvents);
                                player.GetComponent<PlayerController>().EnableAllAbilities();
                                return;
                            }

                            dialogueUI.SetDialogue(nextDialogue,voice);
                            nextDialogue = nextDialogue.GetNextDialogue();
                        }

                    }
                    else
                    {
                        player.GetComponent<PlayerController>().DisableAllAbilities();
                        isInDialogue = true;
                        dialogueUI.SetDialogue(startingDialogue, voice);
                        nextDialogue = startingDialogue.GetNextDialogue();
                        startOfDialogueEvents.Invoke();
                    }
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
}
