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

    private Dictionary<SoundPacket, GameObject> loopAudioList = new Dictionary<SoundPacket, GameObject>();
    private Dictionary<SoundPacket, GameObject> delayAudioList = new Dictionary<SoundPacket, GameObject>();
    private Dictionary<SoundPacket, GameObject> playOnceAudioList = new Dictionary<SoundPacket, GameObject>();

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
                loopAudioList.Add(sound, soundObject);
                soundObject.GetComponent<AudioSource>().loop = true;
                break;

            case SoundType.PlayOnce:
                playOnceAudioList.Add(sound, soundObject);
                break;

            case SoundType.ReplayAfterSeconds:
                delayAudioList.Add(sound, soundObject);
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
                soundObject.GetComponent<AudioSource>().outputAudioMixerGroup =audioMixerSfx;
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
            soundObject.GetComponent<AudioSource>().enabled = false;
            soundObject.GetComponent<AudioSource>().volume = 0;
            soundObject.GetComponent<AudioSource>().enabled = true;
            emission.FadeIn(sound);
            emission.deleteGameObject(spawnedSound);
        }
    }

    public void RemoveSound(SoundPacket sound, bool fade = false)
    {
        if (fade == false)
        {
            emission.StopAudio();
        }

        else

        {
            emission.FadeOut(sound);
        }

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
    }
}
