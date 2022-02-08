using System.Collections;
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

    private Dictionary<SoundPacket, SoundEmissionManager> EmissionSound = new Dictionary<SoundPacket, SoundEmissionManager>();
    
    public void AddSound(SoundPacket sound, bool fade = false)
    {
        SoundType type = sound.GetAudioType();
        OutputType outputType = sound.GetOutputType();
        Vector3 position = sound.GetPlayPosition();

        //Audio Creation

        GameObject soundObject = new GameObject();

        AudioSource audio = soundObject.AddComponent<AudioSource>();
        SoundEmissionManager sem = soundObject.AddComponent<SoundEmissionManager>();
        audio.clip = sound.GetAudio();

        if (!EmissionSound.ContainsKey(sound))
        {
            EmissionSound.Add(sound, sem);
        }

        //Output Type Definition

        switch (outputType)
        {
            case OutputType.Master:
                audio.outputAudioMixerGroup = audioMixerMaster;
                break;

            case OutputType.Music:
                audio.outputAudioMixerGroup = audioMixerMusic;
                break;

            case OutputType.Sfx:
                audio.outputAudioMixerGroup = audioMixerSfx;
                break;
        }

        //Audio Type Management

        if (sound.GetAudioType().Equals(SoundType.Loop))
        {
            audio.loop = true;
        }
        else if (sound.GetAudioType().Equals(SoundType.PlayOnce))
        {
            StartCoroutine(PlayOnceAudioMonitor(audio));
        }

        //Fading

        if (!fade)
        {
            sem.PlayAudio();
        }
        else
        {
            audio.volume = 0;
            audio.enabled = true;
            sem.PlayAudio();
            soundObject.GetComponent<SoundEmissionManager>().FadeIn();
        }
    }

    public void RemoveSound(SoundPacket sound, bool fade = false)
    {
        if (EmissionSound.ContainsKey(sound)) { 
            if (fade)
            {
                EmissionSound[sound].FadeOut();
            }
            else
            {
                Destroy(EmissionSound[sound].gameObject);
            }

            EmissionSound.Remove(sound);
        }
    }

    private IEnumerator PlayOnceAudioMonitor(AudioSource audio)
    {
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(audio.gameObject);
    }
}
