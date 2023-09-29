using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _gameObjectToSpawn;
    [SerializeField] private List<GameObject> _spawnedGameObject;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _spawnCount;
    
    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    void RemoveFromObjects(GameObject removingGameObject)
    {
        _spawnedGameObject.Remove(removingGameObject);
        Destroy(removingGameObject);
    }
    
    IEnumerator SpawnObject()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject newObject = Instantiate(_gameObjectToSpawn, transform.position, Quaternion.identity);
            if (TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                enemy.onDeath += RemoveFromObjects;
            }
            _spawnedGameObject.Add(newObject);
        }
       
        yield return new WaitForSeconds(_timeBetweenSpawn);
        StartCoroutine(SpawnObject());
    }
    
    
}
