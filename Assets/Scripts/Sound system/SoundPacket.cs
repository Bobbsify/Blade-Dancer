using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPacket
{
    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private Transform playPos;

    [SerializeField]
    private SoundType audioType;

    [SerializeField]
    private float playDelay;

    public SoundPacket(AudioSource audio, Transform playPos, SoundType audioType, float playDelay = 0)
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

    public SoundType GetAudioType()
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
