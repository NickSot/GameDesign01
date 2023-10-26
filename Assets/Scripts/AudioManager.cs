using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField]
    public AudioSource musicSource;

    [SerializeField]
    public AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip Background;
    public AudioClip Jump;

    public static AudioManager Instance;

    public AudioManager()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        musicSource.mute = !musicSource.mute;
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
