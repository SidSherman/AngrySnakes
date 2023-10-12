using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaleryManager : MonoBehaviour
{
    [SerializeField] private List<GalleryElement> _elements;
    public Progress ProgressInstance;
    void Awake()
    {
        ProgressInstance = Progress.ProgressInstance;

        GalleryElement[] elements = GetComponentsInChildren<GalleryElement>();
        
        _elements.AddRange(elements);
    }


    public void UpdateElements()
    {
        for (int i = 0; i < _elements.Count; i++)
        {
            if (_elements[i] != null)
            {
                if (ProgressInstance.GetLevel() <= i)
                {
                    break;
                }
                _elements[i].UnlockImage();
            }
        }
        
    }
}
