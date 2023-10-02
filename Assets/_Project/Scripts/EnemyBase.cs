using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _agentComponent;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private List<AudioClip> _deathSounds;

    public delegate void GObjDelegate(GameObject value);

    public delegate void VoidDelegate();

    public event GObjDelegate onDeath;
    public event VoidDelegate OnPlayerKill;


    // Start is called before the first frame update
    void Start()
    {
        if (!_gameManager)
            _gameManager = GameManager.GameManagerInstance;

        
        StartCoroutine(DestinationUpdate(1.0f));
    }


    IEnumerator DestinationUpdate(float updateTime)
    {

        if (!_agentComponent)
        {
            _agentComponent = GetComponent<NavMeshAgent>();
        }

        if (!_target)
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        if (_target && _agentComponent)
        {
            _agentComponent.destination = _target.transform.position;
        }

        yield return new WaitForSeconds(updateTime);

        StartCoroutine(DestinationUpdate(updateTime));
    }

    public void Death()
    {
        Debug.Log("Death");
        if (gameObject)
        {
            if (onDeath != null)
                onDeath(gameObject);
        }

        if (_gameManager.SoundManager)
        {
            _gameManager.SoundManager.PlaySound(_deathSounds[Random.Range(0,_deathSounds.Count-1)]);
        }
    }

  


private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if(other.gameObject.CompareTag("Player"))
        {
            if(OnPlayerKill != null) 
                OnPlayerKill();
        }
    }
}
