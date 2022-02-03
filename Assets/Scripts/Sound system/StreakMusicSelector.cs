using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakMusicSelector : MonoBehaviour
{
    [SerializeField]
    private SoundPacket[] soundPackets;

    [SerializeField]
    [Range(1,5)]
    [Tooltip("Amount of songs needed for a song to be able to be repeated")]
    private int repeatRange = 2;

    private int[] repeatedSongsIndexes;
   
    private int randomPacketIndex;

    private void Awake()
    {
        repeatedSongsIndexes = new int[repeatRange];
        //Get all numbers out of possible range
        for (int i = 0; i < repeatedSongsIndexes.Length; i++) 
        {
            repeatedSongsIndexes[i] = -1; 
        }
    }

    public SoundPacket GetMusic()
    {

        do
        {
            randomPacketIndex = UnityEngine.Random.Range(0, soundPackets.Length);
        } while (CheckRepeated());

        ShiftIndexList();

        repeatedSongsIndexes[0] = randomPacketIndex;

        return soundPackets[repeatedSongsIndexes[0]];
    }

    private bool CheckRepeated()
    {
        foreach (int i in repeatedSongsIndexes) 
        {
            if (randomPacketIndex == i) 
            {
                return true;
            }
        }
        return false;
    }

    private void ShiftIndexList()
    {
        //Shift All
        for (int i = repeatRange-1; i != 0 && repeatRange > 1; i--) 
        {
            repeatedSongsIndexes[i] = repeatedSongsIndexes[i - 1];
        }
    }
}
