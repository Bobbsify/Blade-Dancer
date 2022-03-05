using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Slider))]
public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup mixer;

    private Slider volume;

    [SerializeField]
    private GameObject gameObjectButton;

    private float currentVolume;
    public void ChangeVolume(float value)
    {
        TryGetComponent(out volume);
        currentVolume = value;
        mixer.audioMixer.SetFloat(mixer.name + "Volume", Mathf.Log10(currentVolume) * 20);
        PlayerPrefs.SetFloat(mixer.name + "Volume",volume.value);
        gameObjectButton.GetComponent<Animator>().SetTrigger("Highlighted");
    }

    public void SetupVolume()
    {
        TryGetComponent(out volume);
        volume.value = PlayerPrefs.GetFloat(mixer.name + "Volume", volume.value);
        ChangeVolume(volume.value);
    }
}
