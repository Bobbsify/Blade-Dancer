﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundQueueManager : MonoBehaviour
{

    [SerializeField]
    AudioMixerGroup audioMixerMaster;

    [SerializeField]
    AudioMixerGroup audioMixerMusic;

    [SerializeField]
    AudioMixerGroup audioMixerSfx;

    private Dictionary<SoundPacket, GameObject> loopAudioList = new Dictionary<SoundPacket, GameObject>();
    private Dictionary<SoundPacket, GameObject> delayAudioList = new Dictionary<SoundPacket, GameObject>();
    private Dictionary<SoundPacket, GameObject> playOnceAudioList = new Dictionary<SoundPacket, GameObject>();
    private Dictionary<SoundPacket, SoundEmissionManager> EmissionSound = new Dictionary<SoundPacket, SoundEmissionManager>();

    private SoundEmissionManager emission;
    
    public void AddSound(SoundPacket sound, bool fade = false)
    {
        SoundType type = sound.GetAudioType();
        OutputType outputType = sound.GetOutputType();
        Vector3 position = sound.GetPlayPosition();

        GameObject soundObject = new GameObject();

        soundObject.AddComponent<AudioSource>();
        soundObject.AddComponent<SoundEmissionManager>();
        soundObject.GetComponent<AudioSource>().clip = sound.GetAudio();

        emission = soundObject.GetComponentInChildren<SoundEmissionManager>();

        GameObject spawnedSound = Instantiate(soundObject, position, this.transform.rotation, null);

        switch (type)
        {
            case SoundType.Loop:

                if(loopAudioList.ContainsKey(sound) == false)
                {
                    loopAudioList.Add(sound, soundObject);
                    soundObject.GetComponent<AudioSource>().loop = true;

                    if(EmissionSound.ContainsKey(sound)== false)
                    {
                        EmissionSound.Add(sound, emission);
                    }
                }

                break;

            case SoundType.PlayOnce:

                if(playOnceAudioList.ContainsKey(sound) == false)
                {
                    playOnceAudioList.Add(sound, soundObject);

                    if (EmissionSound.ContainsKey(sound) == false)
                    {
                        EmissionSound.Add(sound, emission);
                    }
                }
            
                break;

            case SoundType.ReplayAfterSeconds:

                if (delayAudioList.ContainsKey(sound) == false)
                {
                    delayAudioList.Add(sound, soundObject);

                    if (EmissionSound.ContainsKey(sound) == false)
                    {
                        EmissionSound.Add(sound, emission);
                    }
                }

                break;
        }

        switch (outputType)
        {
            case OutputType.Master:
                soundObject.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerMaster;
                break;

            case OutputType.Music:
                soundObject.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerMusic;
                break;

            case OutputType.Sfx:
                soundObject.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerSfx;
                break;
        }


        if (fade == false && type == SoundType.PlayOnce)
        {
            emission.PlayAudioOnce();
            emission.deleteGameObject(spawnedSound);
        }

        else if(fade == false && type != SoundType.PlayOnce)
        {
            emission.PlayAudio();
            emission.deleteGameObject(spawnedSound);
        }

        else

        {
            soundObject.GetComponent<AudioSource>().volume = 0;
            soundObject.GetComponent<AudioSource>().enabled = true;
            soundObject.GetComponent<SoundEmissionManager>().FadeIn(sound);
        }
    }

    public void RemoveSound(SoundPacket sound, bool fade = false)
    {
        EmissionSound.Remove(sound);

        SoundType type = sound.GetAudioType();

        switch (type)
        {
            case SoundType.Loop:

                if (!fade)
                {
                    if (loopAudioList.ContainsKey(sound)) 
                    { 
                        Destroy(loopAudioList[sound]);
                        loopAudioList.Remove(sound);
                    }
                }
                else
                {
                    if (loopAudioList.ContainsKey(sound))
                    {
                        loopAudioList[sound].GetComponent<SoundEmissionManager>().FadeOut(sound);
                        loopAudioList.Remove(sound);
                    }
                }
                   
                break;

            case SoundType.PlayOnce:

                if (!fade)
                {
                    if (playOnceAudioList.ContainsKey(sound))
                    {
                        Destroy(playOnceAudioList[sound]);
                        playOnceAudioList.Remove(sound);
                    }
                }
                else
                {
                    if (playOnceAudioList.ContainsKey(sound))
                    {
                        playOnceAudioList[sound].GetComponent<SoundEmissionManager>().FadeOut(sound);
                        playOnceAudioList.Remove(sound);
                    }
                }

                break;

            case SoundType.ReplayAfterSeconds:

                if (!fade)
                {
                    if (delayAudioList.ContainsKey(sound))
                    {
                        Destroy(delayAudioList[sound]);
                        delayAudioList.Remove(sound);
                    }
                }
                else
                {
                    if (delayAudioList.ContainsKey(sound))
                    {
                        delayAudioList[sound].GetComponent<SoundEmissionManager>().FadeOut(sound);
                        delayAudioList.Remove(sound);
                    }
                }

                break;
        }
    }
}
