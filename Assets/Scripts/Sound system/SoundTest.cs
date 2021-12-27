using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTest : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private Transform playPos;

    [SerializeField]
    private SoundType audioType;

    [SerializeField]
    private float playDelay;

    private SoundQueueManager sqm=new SoundQueueManager();
    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            sqm.AddSound(new SoundPacket(audio, playPos, audioType, playDelay));
        }
    }
}
