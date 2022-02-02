﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
            
            GameObject gameManager = GameObject.Find("GameManager");
            if (gameManager != null) 
            {
                sqm = gameManager.GetComponent<SoundQueueManager>();
            }
        }
    }

    public void SetDialogue(Dialogue dialogue)
    {
        StopAllCoroutines();
        EndTelling();
        sqm.RemoveSound(speakingSound);
        gameObject.SetActive(true);

        ImageToShow.sprite = dialogue.GetPicture();

        NameToShow.text = dialogue.GetName();

        totalDialogue = dialogue.GetLine().ToCharArray();
        DialogueText.text = "";
        StartCoroutine(TellDialogue());
        sqm.AddSound(speakingSound);
    }

    public void EndDialogue(UnityEvent events)
    {
        EndTelling();
        StopAllCoroutines();
        gameObject.SetActive(false);
        events.Invoke();
    }

    private IEnumerator TellDialogue() 
    {
        DialogueText.text += totalDialogue[pointInDialogue];
        ++pointInDialogue;
        if (pointInDialogue == totalDialogue.Length)
        {
            EndTelling();
        }
        else { 
            yield return new WaitForSeconds(dialogueSpeed);
            StartCoroutine(TellDialogue());
        }
    }

    private void EndTelling()
    {
        pointInDialogue = 0;
        sqm.RemoveSound(speakingSound);
    }
}
