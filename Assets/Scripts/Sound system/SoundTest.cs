using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTest : MonoBehaviour
{
    [SerializeField] SoundPacket test;
    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            GetAudioType();
        }
    }

    void GetAudioType()
    {

    }
}
