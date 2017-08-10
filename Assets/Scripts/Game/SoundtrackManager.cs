using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public AudioClip[] songs;
    public float minLength;

    private bool shuffle;

    private int currentSong = -1;
    private int loopsRemaining;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(songs.Length > 1)
        {
            shuffle = true;
            audioSource.loop = false;
        }
        else
        {
            shuffle = false;
            currentSong = 0;

            audioSource.loop = true;
            audioSource.clip = songs[0];
            audioSource.Play();
        }
    }

    private void Update()
    {
        if(shuffle)
        {
            if(!audioSource.isPlaying)
            {
                if(loopsRemaining > 0)
                {
                    loopsRemaining--;
                    audioSource.Play();
                }
                else
                {
                    PlayNextSong();
                }
            }
        }
    }

    private void PlayNextSong()
    {
        int oldSong = currentSong;

        do
        {
            currentSong = Random.Range(0, songs.Length);
        } while(currentSong == oldSong);

        loopsRemaining = GetNumLoops();
        audioSource.clip = songs[currentSong];
        audioSource.Play();
    }

    private int GetNumLoops()
    {
        AudioClip song = songs[currentSong];

        return (int) (minLength / song.length);
    }
}
