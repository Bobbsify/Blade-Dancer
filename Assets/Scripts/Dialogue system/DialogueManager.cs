using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    Dialogue currentDialogue;

    public void DoDialogue(DialoguePacket dialoguePerformed)
    {
        currentDialogue = dialoguePerformed.getNextLine();
        Debug.Log(dialoguePerformed);
        //FindObjectOfType<DialoguePacket>().StartDialogue(currentDialogue);
    }
}
