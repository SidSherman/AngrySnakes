using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _agentComponent;
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

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Application.Quit();
        }
    }
}
