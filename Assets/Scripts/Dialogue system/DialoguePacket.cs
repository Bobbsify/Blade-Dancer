using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialoguePacket : MonoBehaviour
{
    [SerializeField]
    private Dialogue[] dialogueToPerform;
    int pointInDialogue=0;

    public Dialogue getNextLine()
    {
        pointInDialogue = pointInDialogue + 1;
        return dialogueToPerform[pointInDialogue];
    }
    
}
