using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryElement : MonoBehaviour
{
    [SerializeField] private Image _linkedImage;
    [SerializeField] private Sprite _lockedsprite;
    [SerializeField] private Sprite _unlockedsprite;
    [SerializeField] private Menu _menu;
    
    private bool _isOpened;

    private void Start()
    {
        if (_linkedImage)
        {
            _linkedImage.sprite = _lockedsprite;
        }
        else
        {
            TryGetComponent<Image>(out Image _linkedImage);
        }
        if (_linkedImage)
        {
            _linkedImage.sprite = _lockedsprite;
        }

        if (!_menu)
        {
            _menu = GetComponentInParent<Menu>();
        }
    }

    public void UnlockImage()
    {
        if (_linkedImage)
        {
            _linkedImage.sprite = _unlockedsprite;
        }
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
