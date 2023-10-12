using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField]
    public AudioSource musicSource;

    [SerializeField]
    public AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip Background;
    public AudioClip Jump;

    public static AudioManager Instance;

    public AudioManager() { 
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

