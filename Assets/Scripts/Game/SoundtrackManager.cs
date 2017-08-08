using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public AudioClip[] songs;
    public float minLength;

    private int currentSong = -1;
    private int loopsRemaining;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
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
