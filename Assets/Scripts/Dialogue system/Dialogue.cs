using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    private string Line;
    private Sprite image;
    private UnityEvent dialogueEvent;

    public string GetLine()
    {
        return Line;
    }

    public Sprite GetImage()
    {
        return image;
    }

    public UnityEvent GetEvent()
    {
        return dialogueEvent;
    }
}
