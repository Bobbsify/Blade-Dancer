using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTest : MonoBehaviour
{
    [SerializeField]
    private SoundPacket sound;

    [SerializeField]
    private SoundQueueManager sqm = new SoundQueueManager();

    [SerializeField]
    private bool fade;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            sqm.AddSound(this.sound,fade);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            sqm.RemoveSound(this.sound, fade);
        }
    }
}
