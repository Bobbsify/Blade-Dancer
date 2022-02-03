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

    public void StopAudio(Dictionary<SoundPacket, SoundEmissionManager> soundEmission,SoundPacket sound)
    {
        soundEmission.Remove(sound);
        audio.Stop();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInTime());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutTime());
    }

    private IEnumerator FadeInTime()
    {
        audio.volume += fadeAmount;

        yield return new WaitForSeconds(fadeTime / (1/fadeAmount));

        if (audio.volume < 0.99f)
        {
            StartCoroutine(FadeInTime());
        }
    }

    private IEnumerator FadeOutTime()
    {
        audio.volume -= fadeAmount;

        yield return new WaitForSeconds(fadeTime / (1 / fadeAmount));

        if (audio.volume > 0)
        {
            StartCoroutine(FadeOutTime());
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void deleteGameObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }
}
