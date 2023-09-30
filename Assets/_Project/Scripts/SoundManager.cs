using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _defaultSource;
    [SerializeField] private AudioSource _defaultMusicSource;
    [SerializeField] private AudioListener _defaultListener;

    public AudioSource DefaultSource => _defaultSource;
    public AudioSource DefaultMusicSource => _defaultMusicSource;
    public AudioListener DefaultListener => _defaultListener;

    static public SoundManager SoundManagerInstance;

    private void Awake()
    {
        SoundManagerInstance = this;
    }

    public void PlayStopMusic(bool value)
    {
        switch (value)
        {
            case true:
                _defaultMusicSource.Play();
                break;
            case false:
                _defaultMusicSource.Stop();
                break;
        }
    }
    
    public void PlaySound(AudioClip clip)
    {
        if(clip)
            _defaultSource.PlayOneShot(clip);
    }
    
    public void PlaySound(AudioClip clip, AudioSource source)
    {
        source.PlayOneShot(clip);
    }
    
    public void ToggleSound(bool value)
    {
        _defaultListener.enabled = value;
    }
    public void ToggleSound()
    {
        _defaultListener.enabled = !_defaultListener.enabled;
    }
}