using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSources;

    private List<AudioSource> sources = new List<AudioSource>();

    private int RandomMusicSelector;

    private void Start()
    { 
        CompileSources();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetSong();
        }
    }

    public AudioSource GetSong()
    {
        AudioSource[] audios = this.audioSources;

        if(sources.Count < 1)
        {
            CompileSources();
        }

        do
        {
            RandomMusicSelector = Random.Range(0, audioSources.Length);
        } while (!sources.Contains(audios[RandomMusicSelector]));

         sources.Remove(audios[RandomMusicSelector]);
      
         audios[RandomMusicSelector].Play();
            
         return audios[RandomMusicSelector];
    }

    private void CompileSources()
    {
       for (int i = 0; i < audioSources.Length; i++)
       {
           sources.Add(audioSources[i]);
       }
    }
}
