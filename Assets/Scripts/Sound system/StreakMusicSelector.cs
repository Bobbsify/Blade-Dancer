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
    [Tooltip("Amount of songs needed for a song to be able to be repeated")]
    private int repeatRange = 2;

    private List<int> repeatedSongsIndexes = new List<int>();
   
    private int randomPacketIndex;

    public SoundPacket GetMusic()
    {

        do
        {
            randomPacketIndex = UnityEngine.Random.Range(0, soundPackets.Length);
        } while (repeatedSongsIndexes.Contains(randomPacketIndex));

        ShiftIndexList();

        if (repeatedSongsIndexes.Count > 0)
        {
            repeatedSongsIndexes[0] = randomPacketIndex;
        }
        else 
        {
            repeatedSongsIndexes.Add(randomPacketIndex);
        }

        return soundPackets[repeatedSongsIndexes[0]];
    }

    private void ShiftIndexList()
    {
        //Remove Last Element
        if (repeatedSongsIndexes.Count > 0)
        {
            repeatedSongsIndexes.Remove(repeatedSongsIndexes[repeatedSongsIndexes.Count - 1]);

            //Shift All
            for (int i = repeatRange-1; i != 1; i--) 
            {
                repeatedSongsIndexes[i] = repeatedSongsIndexes[i - 1];
            }
        }
    }
}
