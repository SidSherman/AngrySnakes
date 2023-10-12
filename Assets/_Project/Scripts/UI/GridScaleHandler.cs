using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GridScaleHandler : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Vector2 _gridDivision;
    
    [SerializeField] private Vector2 _cellSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float widthPanel = _container.GetComponent<RectTransform>().rect.width;
        float heightPanel = _container.GetComponent<RectTransform>().rect.height;
        float min = Mathf.Min(widthPanel, heightPanel);
        _cellSize = new Vector2(min / _gridDivision.x, min / _gridDivision.y);
        _container.GetComponent<GridLayoutGroup>().cellSize = _cellSize;
    }
}
