using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static ObjectPoolingManager Instance { get; private set; }

    [SerializeField]
    private GameObject pooledObject = null;
    [SerializeField]
    private int amountToPool = 10;
    [SerializeField]
    private bool canGrow = false;

    private List<GameObject> poolOfObjects = new List<GameObject>();


    private void Awake () {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        for (int i = 0; i < amountToPool; i++) {
            GameObject newObject = Instantiate(pooledObject, transform);
            newObject.SetActive(false);
            poolOfObjects.Add(newObject);
        }
    }
    
    public GameObject GetPooledObject () {
        for (int i = 0; i < poolOfObjects.Count; i++) {
            if (!poolOfObjects[i].activeInHierarchy) {
                return poolOfObjects[i];
            }
        }

        if (canGrow) {
            GameObject newObject = Instantiate(pooledObject, transform);
            newObject.SetActive(false);
            poolOfObjects.Add(newObject);
            return newObject;
        }

        return null;
    }


}
