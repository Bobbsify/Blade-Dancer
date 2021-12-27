using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundQueueManager
{
    private List<SoundPacket> loopAudioList= new List<SoundPacket>();
    private List<SoundPacket> delayAudioList= new List<SoundPacket>();
    private List<SoundPacket> playOnceAudioList=new List<SoundPacket>();
    private SoundEmissionManager emission;

    public void AddSound(SoundPacket sound, bool fade=false)
    {
        if(fade==false)
        {
            playOnceAudioList.Add(sound);
            emission.PlayAudio(sound);
        }
        else
        {
            playOnceAudioList.Add(sound);
            emission.FadeIn(sound);
        }
    }

    public void RemoveSound(SoundPacket sound, bool fade=false)
    {
        if(fade==false)
        {
            playOnceAudioList.Remove(sound);
            emission.StopAudio(sound);
        }
        else
        {
            playOnceAudioList.Add(sound);
            emission.FadeOut(sound);
        }
    }

    public void ReplaceSound(SoundPacket oldSound, SoundPacket newSound, bool fade=false)
    {
        playOnceAudioList.Remove(oldSound);
        playOnceAudioList.Add(newSound);
    }
}
