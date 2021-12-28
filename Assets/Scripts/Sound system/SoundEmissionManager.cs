using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEmissionManager : MonoBehaviour
{
    //private SoundPacket soundToPlay;

    public void PlayAudio(SoundPacket soundToPlay)
    {
        AudioSource audio = soundToPlay.GetAudio();
        audio.Play();
    }   
    public void StopAudio(SoundPacket soundToStop)
    {
        AudioSource audio = soundToStop.GetAudio();
        audio.Stop();
    }
    public void FadeIn(SoundPacket soundToFadeIn, float fadeDuration=1)
    {

    }
    public void FadeOut(SoundPacket soundToFadeOut, float fadeduration=1)
    {

    }
}
