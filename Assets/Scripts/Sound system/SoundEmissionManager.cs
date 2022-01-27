using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundEmissionManager : MonoBehaviour
{
    private new AudioSource audio;

    private const float fadeAmount = 0.1f;

    private const float fadeTime = 3f;

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
    }

    public void StopAudio(SoundPacket soundPacket)
    {
        audio.Stop();
    }

    private void Update()
    {
        if(this.gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            Destroy(this.gameObject);
        }
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
        audio.volume += fadeAmount;

        fadeDuration = fadeTime;

        yield return new WaitForSeconds(fadeDuration / (1/fadeAmount));

        if (audio.volume < 0.99f)
        {
            StartCoroutine(FadeInTime(sound, fadeDuration));
        }
    }

    private IEnumerator FadeOutTime(SoundPacket sound, float fadeDuration)
    {
        audio.volume -= fadeAmount;

        fadeDuration = fadeTime;

        yield return new WaitForSeconds(fadeDuration / (1/fadeAmount));

        if(audio.volume > 0)
        {
            StartCoroutine(FadeOutTime(sound, fadeDuration));
        }
    }

    public void deleteGameObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }
}
