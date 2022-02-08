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
    [Tooltip("(Optional) Replaces dialogue voice with this one")]
    private SoundPacket voiceOverride;

    [SerializeField]
    private Dialogue nextDialogue;

    public Dialogue(string name, string line, Sprite portrait, Dialogue nextDialogue, SoundPacket voiceOverride = null)
    {
        this.characterName = name;
        this.characterLine = line;
        this.characterPortrait = portrait;
        this.voiceOverride = voiceOverride;
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
    public SoundPacket GetVoice()
    {
        return this.voiceOverride;
    }

    public Dialogue GetNextDialogue()
    {
        return this.nextDialogue;
    }
}
