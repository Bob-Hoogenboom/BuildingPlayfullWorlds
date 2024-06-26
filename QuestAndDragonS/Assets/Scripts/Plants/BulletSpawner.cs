using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// # Why do we even have this if it gets destroyted with the peashooter object???
/// # Just instantiate a bullet! You premature optimizationing f@cker!!!
/// </summary>
public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner instance;
    
    private List<GameObject> _pooledObjects = new List<GameObject>();
    private int _amountInPool = 3;

    [SerializeField] private GameObject bulletPrefab;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (_pooledObjects.Count >= _amountInPool) return;

        for (int i = 0; i < _amountInPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        return null;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (_pooledObjects[i] == null) return;
            if (!_pooledObjects[i].activeInHierarchy)
            {
                Destroy(_pooledObjects[i]);
            }
        }

        _pooledObjects.Clear();
    }
}
