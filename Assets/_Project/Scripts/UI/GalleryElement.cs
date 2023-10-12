using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryElement : MonoBehaviour
{
    [SerializeField] private Image _linkedImage;
    [SerializeField] private Image _previewImage;
    [SerializeField] private Sprite _lockedsprite;
    [SerializeField] private Sprite _unlockedsprite;
    [SerializeField] private Menu _menu;
    
    [SerializeField] private bool _isOpened = false;

    private void Start()
    {
        
        
        if (!_menu)
        {
            _menu = GetComponentInParent<Menu>();
        }

        if (!_previewImage)
        {
            TryGetComponent<Image>(out Image _previewImage);
        }
        if (_previewImage)
        {
            if(!_isOpened)
                _previewImage.sprite = _lockedsprite;
            else
            {
                _previewImage.sprite = _unlockedsprite;
            }
        }

    }

    public void UnlockImage()
    {
        if (_linkedImage)
        {
            _linkedImage.sprite = _unlockedsprite;
        }

        if (_previewImage)
        {
            _previewImage.sprite = _unlockedsprite;
        }
        _isOpened = true;
    }
    
    
    public void ShowImage()
    {
        if (_isOpened)
        {
            if(_menu)
                _menu.ShowImage(_unlockedsprite);
        }
    }
  
}
