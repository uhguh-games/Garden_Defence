
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private AudioSource musicSource, sfxSource;
    public static AudioManager instance;


    void OnEnable()
    {
        eventManager.playMusic += PlayMusic;
        eventManager.playSFX += PlaySFX;
    }

    void Start()
    {
        PlayMusic(5); //Play the one song we have
    }
    public Soundbank soundBank;

    public void PlayMusic(int i)
    {
        //find selected track
        AudioClip s = soundBank.soundbankArray[i];
        //check if file exists
        switch (s)
        {
            case null:
                Debug.Log("No such song");
                break;
            default:
                musicSource.clip = s;
                musicSource.Play();
                
                break;
        }
    }
    
    public void PlaySFX(int i)
    {
        //find selected track
        AudioClip s = soundBank.soundbankArray[i];
        //check if file exists
        switch (s)
        {
            case null:
                Debug.Log("No such song");
                break;
            default:
                sfxSource.clip = s;
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
