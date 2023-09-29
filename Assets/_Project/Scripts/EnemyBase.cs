using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _agentComponent;
    
    public delegate void GObjDelegate(GameObject value);
    public event GObjDelegate onDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(DestinationUpdate(1.0f));
    }


    IEnumerator DestinationUpdate(float updateTime)
    {
        
        if(!_agentComponent)
        {
            _agentComponent = GetComponent<NavMeshAgent>();
        }
        if(!_target)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        if(_target && _agentComponent)
        {
            _agentComponent.destination = _target.transform.position;
        }
        
        yield return new WaitForSeconds(updateTime);
        
        StartCoroutine(DestinationUpdate(updateTime));
    }


    private void OnMouseDown()
    {
        if (gameObject)
        {
            onDeath(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Application.Quit();
        }
    }
}
