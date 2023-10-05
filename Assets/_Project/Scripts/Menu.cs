using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] protected GameObject _galleryPanel;
    [SerializeField] protected GameObject _creditsPanel;
    
    [SerializeField] protected Image _galleyImage;
    [SerializeField] protected TextMeshProUGUI _scoreText;
    
    
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
        
        UpdateScore();
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

    public void ToggleCreditsPanel()
    {
        if (_creditsPanel)
        {
            _creditsPanel.SetActive(!_creditsPanel.activeSelf);
        }
    }
    
    public void ToggleGalleryPanel()
    {
        if (_galleryPanel)
        {
            _galleryPanel.SetActive(!_galleryPanel.activeSelf);
            if(_creditsPanel && _galleryPanel.activeSelf)
                _creditsPanel.SetActive(false);
        }
    }
    
    public void ShowImage(Sprite imageSource)
    {
        if (_galleyImage)
        {
            _galleyImage.gameObject.SetActive(true);
            if (imageSource)
            {
                _galleyImage.sprite = imageSource;
            }
        }
    }
    
    public void CloseImage()
    {
        if (_galleyImage)
        {
            _galleyImage.gameObject.SetActive(false);
        }
    }

    public void UpdateScore()
    {
        if(Progress.ProgressInstance)
            Debug.Log("Progress Instance is valid");
        if(Progress.ProgressInstance.LevelsInfo != null)
            Debug.Log("Progress Instance Level Info is valid");
        if(Progress.ProgressInstance.LevelsInfo[Progress.ProgressInstance.GetLevel()].NeededScore != null)
            Debug.Log("NeededScore is valid");
        
        _scoreText.text = $"Ваши змейки\n{Progress.ProgressInstance.GetScore()}/{Progress.ProgressInstance.LevelsInfo[Progress.ProgressInstance.GetLevel()].NeededScore}";
    }
    
    public void OpenMyURL(string url)
        {
            Application.OpenURL(url);
        }
}