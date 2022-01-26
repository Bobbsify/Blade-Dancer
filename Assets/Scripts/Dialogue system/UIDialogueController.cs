using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueController : MonoBehaviour
{

    [SerializeField]
    [Range(0.01f,1.0f)]
    private float dialogueSpeed = 0.01f;

    [Header("Dialogue Sound")]

    [SerializeField]
    private SoundQueueManager sqm;

    [SerializeField]
    private SoundPacket speakingSound;

    [Header("UI")]
    [SerializeField]
    private Image ImageToShow;

    [SerializeField]
    private Text NameToShow;

    [SerializeField]
    private Text DialogueText;

    private char[] totalDialogue;
    private int pointInDialogue = 0;

    private void OnValidate()
    {
        if (sqm == null) 
        {
            sqm = transform.root.GetComponentInChildren<SoundQueueManager>();
        }
    }

    public void SetDialogue(Dialogue dialogue)
    {
        gameObject.SetActive(true);

        ImageToShow.sprite = dialogue.GetPicture();

        NameToShow.text = dialogue.GetName();

        totalDialogue = dialogue.GetLine().ToCharArray();
        DialogueText.text = "";
        StartCoroutine(TellDialogue());
    }

    public void EndDialogue()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator TellDialogue() 
    {
        DialogueText.text += totalDialogue[pointInDialogue];
        ++pointInDialogue;
        if (pointInDialogue == totalDialogue.Length)
        {
            pointInDialogue = 0;
        }
        else { 
            yield return new WaitForSeconds(dialogueSpeed);
            StartCoroutine(TellDialogue());
        }
    }
}
