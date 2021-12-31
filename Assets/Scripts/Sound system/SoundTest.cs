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
        if(Input.GetKeyDown(KeyCode.K))
        {
            sqm.AddSound(sound);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            sqm.RemoveSound(sound);
        }
    }
}
