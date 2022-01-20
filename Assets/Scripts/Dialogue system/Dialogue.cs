using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private string characterName = "Default";

    [SerializeField]
    private string characterLine = "Ciaone Proprio";

    [SerializeField]
    private Image characterPortrait;

    [SerializeField]
    private Dialogue nextDialogue;

    public Dialogue(string name, string line, Image portrait, Dialogue nextDialogue) 
    {
        this.characterName = name;
        this.characterLine = line;
        this.characterPortrait = portrait;
        this.nextDialogue = nextDialogue;
    }

    public string GetName()
    {
        return this.characterName;
    }

    public string GetLine()
    {
        return this.characterLine;
    }

    public Image GetPicture()
    {
        return this.characterPortrait;
    }

    public Dialogue GetNextDialogue()
    {
        return this.nextDialogue;
    }
}
