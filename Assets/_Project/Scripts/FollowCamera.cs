using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{

    [SerializeField] private GameObject _target;
    [SerializeField] private float _followSpeed;
    [SerializeField] private Vector3 _offset;


  
    void Awake()
    {
        
        if(_target)
        {
            gameObject.transform.position = _target.transform.position + _offset;
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        if(_target)
        {
            Vector3 newPosition = Vector3.Lerp(gameObject.transform.position, _target.transform.position + _offset, 0.5f * _followSpeed * Time.deltaTime);
            gameObject.transform.position = newPosition;
        }
       
    }
}
