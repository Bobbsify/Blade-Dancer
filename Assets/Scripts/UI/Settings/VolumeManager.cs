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
        // ( (x - -80) / (0 - -80) ) * (1 - 0.0001) + 0.0001 --> Conversione lineare per il nuovo range
        volume.value = (float)((currentVolume - -80) / (0 - -80) * (1 - 0.0001) + 0.0001);
    }

    public void ChangeVolume(float value) 
    {
        currentVolume = value;
        mixer.audioMixer.SetFloat(mixer.name + "Volume", Mathf.Log10(currentVolume) * 20);
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
