using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakMusicSelector : MonoBehaviour
{
    [SerializeField]
    private SoundPacket[] soundPackets;

    private List<int> lastTwoSongIndex = new List<int>();

    private const int maxMusic = 2;
   
    private int getRandomPacketNumber;

    public SoundPacket GetMusic()
    {
        if(lastTwoSongIndex.Count > maxMusic)
        {
            lastTwoSongIndex.Remove(lastTwoSongIndex[1]);
        }

        do
        {
            getRandomPacketNumber = UnityEngine.Random.Range(0, soundPackets.Length);
        } while (lastTwoSongIndex.Contains(getRandomPacketNumber));

        lastTwoSongIndex.Add(getRandomPacketNumber);
        lastTwoSongIndex[1] = lastTwoSongIndex[0];
        lastTwoSongIndex[0] = lastTwoSongIndex[getRandomPacketNumber];

        return soundPackets[lastTwoSongIndex[0]];
    }
}
