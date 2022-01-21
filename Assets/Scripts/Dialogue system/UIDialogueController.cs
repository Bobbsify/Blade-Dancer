using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueController : MonoBehaviour
{
    [SerializeField]
    private Image ImageToShow;

    [SerializeField]
    private Text NameToShow;

    [SerializeField]
    private Text DialogueText;

    [SerializeField]
    private GameObject UIdialogue;


    public void SetDialogue(Dialogue dialogue)
    {
        UIdialogue.SetActive(true);

        ImageToShow.sprite = dialogue.GetPicture();

        NameToShow.text = dialogue.GetName();

        DialogueText.text = dialogue.GetLine();
    }

    internal void EndDialogue()
    {
        UIdialogue.SetActive(false);
        Debug.LogError("END OF DIALOGUE");
    }
}
