using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _defaultSource;
    [SerializeField] private AudioSource _defaultMusicSource;
    [SerializeField] private AudioListener _defaultListener;
    private bool _enabledSound = true;

    public bool EnabledSound => _enabledSound;

    public AudioSource DefaultSource => _defaultSource;
    public AudioSource DefaultMusicSource => _defaultMusicSource;
    public AudioListener DefaultListener => _defaultListener;

    static public SoundManager SoundManagerInstance;

    private void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);

        if (SoundManagerInstance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            SoundManagerInstance = this;
        }

       
    }

    private void Start()
    {
        if (!_defaultListener)
            _defaultListener = GetComponent<AudioListener>();
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
        _enabledSound = value;
        if (!_defaultListener)
        {
            if(TryGetComponent(out AudioListener listener))
                _defaultListener = listener;
        }

        if (_defaultListener)
        {
            _defaultListener.enabled = value;
            _defaultSource.mute = !value;
            _defaultMusicSource.mute = !value;
        }
           
        else
        {
            Debug.Log("MY DEBUG _defaultListener invalid");
        }
    }
    public void ToggleSound()
    {
        if (_enabledSound)
        {
            _enabledSound = false;
            _defaultListener.enabled = false;

            _defaultSource.mute = false;
            _defaultMusicSource.mute = false;
        }
        
        else
        {
            _enabledSound = true;
            _defaultListener.enabled = true;

            _defaultSource.mute = true;
            _defaultMusicSource.mute = true;
        }
    }
}