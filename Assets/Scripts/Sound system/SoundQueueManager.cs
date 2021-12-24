using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundQueueManager
{
    private List<SoundPacket> loopAudioList= new List<SoundPacket>();
    private List<SoundPacket> delayAudioList= new List<SoundPacket>();
    private List<SoundPacket> playOnceAudioList=new List<SoundPacket>();

    public void AddSound(SoundPacket sound, bool fade=false)
    {
        
    }

    public void RemoveSound(SoundPacket sound, bool fade=false)
    {

    }

    public void ReplaceSound(SoundPacket oldSound, SoundPacket newSound, bool fade=false)
    {

    }
}
