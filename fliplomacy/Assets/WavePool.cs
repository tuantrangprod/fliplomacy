using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePool : MonoBehaviour
{
    public static WavePool SharedInstance;
    public List<GameObject> pooledObjects;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            pooledObjects.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
