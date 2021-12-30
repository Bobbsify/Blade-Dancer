using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundEmissionManager : MonoBehaviour
{
    
    private AudioSource audio;
    private void Awake()
    {
        TryGetComponent(out audio);
    }
    public void PlayAudio()
    {
        audio.Play();
    }   

    public void PlayAudioOnce()
    {
        audio.Play();
        Destroy(gameObject);
    }

    public void StopAudio()
    {
        audio.Stop();
    }

    public void FadeIn(SoundPacket soundToFadeIn, float fadeDuration=0)
    {
        StartCoroutine(FadeInTime(soundToFadeIn, fadeDuration));
    }

    public void FadeOut(SoundPacket soundToFadeOut, float fadeDuration=0)
    {
        StartCoroutine(FadeOutTime(soundToFadeOut, fadeDuration));
    }

    private IEnumerator FadeInTime(SoundPacket sound, float fadeDuration)
    {
        fadeDuration = sound.GetDelay();
        float startVolume = audio.volume;
        audio.Play();
        audio.volume = startVolume;
        while (audio.volume < 1)
        {
            audio.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    private IEnumerator FadeOutTime(SoundPacket sound, float fadeDuration)
    {
        fadeDuration = sound.GetDelay();
        float startVolume = audio.volume;
        while(audio.volume>0)
        {
            audio.volume -= startVolume * Time.deltaTime/fadeDuration;
            yield return null;
        }
        audio.Stop();
        audio.volume = startVolume;
        Destroy(gameObject);
    }
}
