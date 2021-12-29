using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundQueueManager 
{
    public List<SoundPacket> loopAudioList= new List<SoundPacket>();
    public List<SoundPacket> delayAudioList= new List<SoundPacket>();
    public List<SoundPacket> playOnceAudioList=new List<SoundPacket>();
    private SoundEmissionManager emission;

    public void AddSound(SoundPacket sound, bool fade=false)
    {
        SoundType type= sound.GetAudioType();
        switch (type)
        {
            case SoundType.Loop:
                loopAudioList.Add(sound);
                break;

            case SoundType.PlayOnce:
                playOnceAudioList.Add(sound);
                break;

            case SoundType.ReplayAfterSeconds:
                delayAudioList.Add(sound);
                break;
        }

        if (fade==false)
        {
            emission.PlayAudio(sound);
        }

        else

        {
            emission.FadeIn(sound);
        }
    }

    public void RemoveSound(SoundPacket sound, bool fade=false)
    {
        SoundType type = sound.GetAudioType();
        switch (type)
        {
            case SoundType.Loop:
                loopAudioList.Remove(sound);
                break;

            case SoundType.PlayOnce:
                playOnceAudioList.Remove(sound);
                break;

            case SoundType.ReplayAfterSeconds:
                delayAudioList.Remove(sound);
                break;
        }

        if (fade==false)
        {
            emission.StopAudio(sound);
        }

        else

        {
            emission.FadeOut(sound);
        }
    }

    public void ReplaceSound(SoundPacket oldSound, SoundPacket newSound, bool fade=false)
    {
        SoundType type = oldSound.GetAudioType();
        switch (type)
        {
            case SoundType.Loop:
                loopAudioList.Remove(oldSound);
                loopAudioList.Add(newSound);
                break;

            case SoundType.PlayOnce:
                playOnceAudioList.Remove(oldSound);
                playOnceAudioList.Add(newSound);
                break;

            case SoundType.ReplayAfterSeconds:
                delayAudioList.Remove(oldSound);
                delayAudioList.Add(newSound);
                break;
        }

        if (fade == false)
        {
            emission.PlayAudio(newSound);
        }

        else

        {
            emission.FadeIn(newSound);
        }
    }
}
