using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private string characterName = "Default";

    [SerializeField]
    private string characterLine = "Ciaone Proprio";

    [SerializeField]
    private Sprite characterPortrait;

    [SerializeField]
    private Dialogue nextDialogue;

    public Dialogue(string name, string line, Sprite portrait, Dialogue nextDialogue) 
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

    public Sprite GetPicture()
    {
        return this.characterPortrait;
    }

    public Dialogue GetNextDialogue()
    {
        return this.nextDialogue;
    }
}
