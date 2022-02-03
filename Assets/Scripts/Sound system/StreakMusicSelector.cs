using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakMusicSelector : MonoBehaviour
{
    [SerializeField]
    private SoundPacket[] soundPackets;

    [SerializeField]
    [Range(2,5)]
    private int maxMusic = 2;

    private List<int> lastTwoSongIndex = new List<int>();
   
    private int randomPacketIndex;

    public SoundPacket GetMusic()
    {

        do
        {
            randomPacketIndex = UnityEngine.Random.Range(0, soundPackets.Length);
        } while (lastTwoSongIndex.Contains(randomPacketIndex));

        ShiftIndexList();

        return soundPackets[lastTwoSongIndex[0]];
    }

    private void ShiftIndexList()
    {
        //Remove Last Element
        lastTwoSongIndex.Remove(lastTwoSongIndex[lastTwoSongIndex.Count - 1]);

        //Shift All
        if (lastTwoSongIndex.Count > 0) 
        { 
            for (int i = maxMusic-1; i != 1; i--) 
            {
                lastTwoSongIndex[i] = lastTwoSongIndex[i - 1];
            }
        }

        //Add Element
        lastTwoSongIndex[0] = randomPacketIndex;
    }
}
