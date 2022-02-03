using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakMusic : MonoBehaviour
{
    [SerializeField]
    private SoundPacket[] soundPackets;

    private List<SoundPacket> sources = new List<SoundPacket>();

    private int RandomMusicSelector;

    private void Start()
    { 
        CompileSources();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetSoundpacketForStreak();
        }
    }

    public SoundPacket GetSoundpacketForStreak()
    {
        SoundPacket[] packets = this.soundPackets;

        if(sources.Count < 1)
        {
            CompileSources();
        }

        do
        {
            RandomMusicSelector = Random.Range(0, packets.Length);
        } while (!sources.Contains(packets[RandomMusicSelector]));

         sources.Remove(packets[RandomMusicSelector]);

         return packets[RandomMusicSelector];
    }

    private void CompileSources()
    {
       for (int i = 0; i < soundPackets.Length; i++)
       {
           sources.Add(soundPackets[i]);
       }
    }
}
