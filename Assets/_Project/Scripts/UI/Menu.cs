using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] protected AudioSource _muicSource;
    [SerializeField] protected AudioListener _listener;

    //Panels
    [SerializeField] protected GameObject _menuPanel;
    [SerializeField] private GameObject _galleryPanel;
    [SerializeField] private GameObject _creditsPanel;
    
    [SerializeField] private Image _galleyImage;
    [SerializeField] protected TextMeshProUGUI _scoreText;
    
    [SerializeField] private GaleryManager _galleryManager;

    [SerializeField] protected SoundManager _soundManagerInstance;
    
    private void Start()
    {
        if (!_soundManagerInstance)
        {
            _soundManagerInstance = SoundManager.SoundManagerInstance;
        }
       
        if (!_sceneManager)
        {
            _sceneManager = SceneLoader.SceneLoaderInstance;
        }

        if (!_galleryManager)
        {
            _galleryManager = FindObjectOfType <GaleryManager>();
        }
        UpdateScore();
    }
    

    public void SoundOnOff()
    {

        if (!_soundManagerInstance)
        {
            Debug.Log("MY DEBUG _soundManagerInstance invalid");
        }
        if(_soundManagerInstance.EnabledSound)
        {
            _soundManagerInstance.ToggleSound(false);
           // _audioSource.mute = false;
            
            //if (!_muicSource)
            //    return;
            //_muicSource.mute = false;
            //_listener.enabled = false;
           
            _soundBttnImage.color = Color.grey;
        }
        else
        {
            _soundManagerInstance.ToggleSound(true);
            //_audioSource.mute = true;
            _soundBttnImage.color = Color.white;
            
            //if (!_muicSource)
            //    return;
            //_muicSource.mute = true;
           // _listener.enabled = true;
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
            if(_galleryPanel && _creditsPanel.activeSelf)
                _galleryPanel.SetActive(false);
        }
    }
    
    public void ToggleGalleryPanel()
    {
        if (_galleryPanel)
        {
            _galleryPanel.SetActive(!_galleryPanel.activeSelf);
            if(_creditsPanel && _galleryPanel.activeSelf)
                _creditsPanel.SetActive(false);
            
            if (_galleryManager)
            {
                _galleryManager.UpdateElements();
            }

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
        if(!Progress.ProgressInstance)
            Debug.Log("Progress Instance invalid");
        if(Progress.ProgressInstance.LevelsInfo == null)
            Debug.Log("Progress Instance Level Info invalid");
    
        _scoreText.text = $"Ваши змейки\n{Progress.ProgressInstance.GetScore()}/{Progress.ProgressInstance.LevelsInfo[Progress.ProgressInstance.GetLevel()].NeededScore}";
        if (_galleryManager)
        {
            _galleryManager.UpdateElements();
        }
        
    }
    
    public void OpenMyURL(string url)
        {
            Application.OpenURL(url);
        }
}