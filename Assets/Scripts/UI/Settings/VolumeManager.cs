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

    private void Awake() 
    {
        TryGetComponent(out volume);
        mixer.audioMixer.GetFloat(mixer.name+"Volume", out currentVolume);
        volume.value = currentVolume;
    }

    public void ChangeVolume(float value) 
    {
        currentVolume = value;
        mixer.audioMixer.SetFloat(mixer.name + "Volume", currentVolume);
        gameObjectButton.GetComponent<Animator>().SetTrigger("Highlighted");
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Vertical") != 0)
        {
            gameObjectButton.GetComponent<Animator>().SetTrigger("Normal");
        }
    }
}
