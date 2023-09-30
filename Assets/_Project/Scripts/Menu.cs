using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] protected SceneLoader _sceneManager;
    
    // Buttons appears
    [SerializeField] protected Image _soundBttnImage;
    [SerializeField] protected Sprite _soundBttnActive;
    [SerializeField] protected Sprite _soundBttnInactive;
    
    // Sounds
    [SerializeField] protected AudioClip _clickSound;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected AudioListener _listener;

    //Panels
    [SerializeField] protected GameObject _menuPanel;

    private void Start()
    {
      
        if (!_audioSource)
        {
            _audioSource = SoundManager.SoundManagerInstance.DefaultSource;
        }
        if (!_listener)
        {
            _listener = SoundManager.SoundManagerInstance.DefaultListener;
        }
        if (!_sceneManager)
        {
            _sceneManager = SceneLoader.SceneLoaderInstance;
        }
    }
    

    public void SoundOnOff()
    {
        if(_listener.enabled == true)
        {
            _listener.enabled = false;
            _soundBttnImage.sprite = _soundBttnInactive;
        }
        else
        {
            _soundBttnImage.sprite = _soundBttnActive;
            _listener.enabled = true;
        }
    }
    
    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }
    
}