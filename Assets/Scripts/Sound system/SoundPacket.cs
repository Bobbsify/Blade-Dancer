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
    private OutputType outputType;

    public SoundPacket(AudioClip audio, Vector3 playPos, SoundType audioType, OutputType outputType)
    {
        this.audio = audio;
        this.playPos = playPos;
        this.audioType = audioType;
        this.outputType = outputType;
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

    public OutputType GetOutputType()
    {
        return outputType;
    }

}
public enum SoundType
{
    Loop,
    PlayOnce,
    ReplayAfterSeconds
}

public enum OutputType
{
    Master,
    Music,
    Sfx
}
