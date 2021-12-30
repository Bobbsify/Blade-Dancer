using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundQueueManager
{
    public Dictionary<SoundPacket, GameObject> loopAudioList= new Dictionary<SoundPacket, GameObject>();
    public Dictionary<SoundPacket, GameObject> delayAudioList= new Dictionary<SoundPacket, GameObject>();
    public Dictionary<SoundPacket, GameObject> playOnceAudioList=new Dictionary<SoundPacket, GameObject>();
    private SoundEmissionManager emission;

    public void AddSound(SoundPacket sound, bool fade=false)
    {
        GameObject soundObject = new GameObject();
        AudioSource audioToAttach = new AudioSource();
        audioToAttach.clip = sound.GetAudio();
        soundObject.AddComponent<AudioSource>();
        SoundType type = sound.GetAudioType();

        switch (type)
        {
            case SoundType.Loop:
                loopAudioList.Add(sound, soundObject);
                break;

            case SoundType.PlayOnce:
                playOnceAudioList.Add(sound, soundObject);
                break;

            case SoundType.ReplayAfterSeconds:
                delayAudioList.Add(sound, soundObject);
                break;
        }

        if (fade==false && type == SoundType.PlayOnce)
        {
            emission.PlayAudioOnce();
        }

        else if(fade==false && type !=SoundType.PlayOnce)
        {
            emission.PlayAudio();
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
            emission.StopAudio();
        }

        else

        {
            emission.FadeOut(sound);
        }
    }

    public void ReplaceSound(SoundPacket oldSound, SoundPacket newSound, bool fade=false)
    {
        RemoveSound(oldSound, fade);
        AddSound(newSound, fade);
    }
}
