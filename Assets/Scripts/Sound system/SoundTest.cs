using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTest : MonoBehaviour
{
    [SerializeField]
    private SoundPacket sound;

    private SoundQueueManager sqm=new SoundQueueManager();
    private void Update()
    {
        if(Input.GetKey(KeyCode.K))
        {
            sqm.AddSound(sound);
        }
    }
}
