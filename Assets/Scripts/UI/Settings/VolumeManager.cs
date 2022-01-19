using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup mixer;

    private Slider volume;

    private float currentVolume;

    private void Awake() 
    {
        mixer.audioMixer.GetFloat(mixer.name, out currentVolume);
        volume.value = currentVolume;
    }

    public void ChangeVolume(float value) 
    {
        currentVolume = value;
        mixer.audioMixer.SetFloat(mixer.name, currentVolume);
    }
}
