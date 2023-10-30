using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip Background;

    private void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        musicSource.clip = Background;
        musicSource.loop = true;
        musicSource.Play();
        //ChangeMasterVolume(1f);
    }
    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume) {
        AudioListener.volume = volume;
    }

    public void ToggleMusic() {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX() {
        musicSource.mute = !musicSource.mute;
    }
    public void MusicVolume(float volume) {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume) {
        SFXSource.volume = volume;
    }
}
