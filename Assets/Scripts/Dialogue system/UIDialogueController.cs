using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogueController : MonoBehaviour
{
    public void SetDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.GetName() + " dice: \"" + dialogue.GetLine() + "\" - Utilizzando " + dialogue.GetPicture());
    }

    internal void EndDialogue()
    {
        Debug.LogError("END OF DIALOGUE");
    }
}
