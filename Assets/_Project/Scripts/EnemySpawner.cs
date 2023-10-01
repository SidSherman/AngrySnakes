using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> _gameObjectToSpawn;
    [SerializeField] private List<GameObject> _spawnedGameObject;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _timeBetweenAcceleration = 10.0f;
    [SerializeField] private float _accelerationDelta = 0.1f;
    [SerializeField] private float _spawnCount;
    [SerializeField] private GameManager _gameManager;
    
    void Start()
    {
       
        if (!_gameManager)
        {
            _gameManager = GameManager.GameManagerInstance;
        }
        StartCoroutine(SpawnDelay(Random.Range(1.0f, _timeBetweenSpawn)));
        StartCoroutine(SpawnTimeDecreaser(_timeBetweenAcceleration));
    }

    void RemoveFromObjects(GameObject removingGameObject)
    {
        _gameManager.UpdateScore(1);
        _spawnedGameObject.Remove(removingGameObject);
        Destroy(removingGameObject);
    }

    IEnumerator SpawnDelay(float value)
    {
        yield return new WaitForSeconds(value);
        StartCoroutine(SpawnObject());
    }
    
    IEnumerator SpawnTimeDecreaser(float value)
    {
        if (_timeBetweenSpawn >= 0.1f)
        {
            _timeBetweenSpawn -= _accelerationDelta;
        }
        yield return new WaitForSeconds(value);
        StartCoroutine(SpawnTimeDecreaser(value));
    }

    IEnumerator SpawnObject()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            if (_gameObjectToSpawn.Count > 0)
            {
                int randomObj =  (int) Random.Range(0.0f, _gameObjectToSpawn.Count);
            
                GameObject newObject = Instantiate(_gameObjectToSpawn[randomObj], transform.position, transform.rotation);
                
                if (newObject.TryGetComponent(out EnemyBase enemy))
                {
                    enemy.onDeath += RemoveFromObjects;
                    enemy.OnPlayerKill += _gameManager.Lose;
                }
                _spawnedGameObject.Add(newObject);
            }
        }
       
        yield return new WaitForSeconds(_timeBetweenSpawn + Random.Range(-_accelerationDelta, _accelerationDelta));
        StartCoroutine(SpawnObject());
    }
    
    
}
