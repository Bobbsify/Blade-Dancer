using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundPacket
{
    [SerializeField]
    private AudioClip audio;

    [SerializeField]
    private Vector3 playPos;

    [SerializeField]
    private SoundType audioType;

    [SerializeField]
    private float playDelay;

    public SoundPacket(AudioClip audio, Vector3 playPos, SoundType audioType, float playDelay = 0)
    {
        this.audio = audio;
        this.playPos = playPos;
        this.audioType = audioType;
        this.playDelay = playDelay;
    }


    public AudioClip GetAudio()
    {
        return audio;
    }

    public Vector3 GetPlayPosition()
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
