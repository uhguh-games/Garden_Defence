using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicTracks, sfxTracks;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        //find selected track
        Sound s = Array.Find(musicTracks, s  => s.FileName == name);
        //check if file exists
        switch (s)
        {
            case null:
                Debug.Log("No such song");
                break;
            default:
                musicSource.clip = s.clip;
                musicSource.Play();
                break;
        }
    }
    public void PlaySFX(string name)
    {
        //find selected track
        Sound s = Array.Find(sfxTracks, s => s.FileName == name);
        //check if file exists
        switch (s)
        {
            case null:
                Debug.Log("No such song");
                break;
            default:
                sfxSource.clip = s.clip;
                sfxSource.Play();
                break;
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
