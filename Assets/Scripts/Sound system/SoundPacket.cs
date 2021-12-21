using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPacket
{
    private AudioSource audio;
    private Transform playPos;
    private SoundType audioType;
    private float playDelay;

    SoundPacket(AudioSource audio, Transform playPos, SoundType audioType, float playDelay = 0)
    {
        this.audio = audio;
        this.playPos = playPos;
        this.audioType = audioType;
        this.playDelay = playDelay;
    }

    public AudioSource GetAudio()
    {
        return audio;
    }

    public Transform GetPlayPosition()
    {
        return playPos;
    }

    public SoundType GetType()
    {
        return audioType;
    }

    public float GetDelay()
    {
        return playDelay;
    }

}
public enum SoundType
{
    Loop,
    PlayOnce,
    ReplayAfterSeconds
}
