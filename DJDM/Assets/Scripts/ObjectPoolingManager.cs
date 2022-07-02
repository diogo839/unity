using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour {

    public static ObjectPoolingManager Instance { get; private set; }

    [SerializeField]
    private GameObject pooledObject = null;
    [SerializeField]
    private GameObject bossPooledObject = null;
    [SerializeField]
    private int amountToPool = 10;
    [SerializeField]
    private bool canGrow = false;

    private List<GameObject> poolOfObjects = new List<GameObject>();
    private List<GameObject> bossPoolOfObjects = new List<GameObject>();


    private void Awake() {
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
        for (int i = 0; i < amountToPool; i++) {
            GameObject newObject = Instantiate(bossPooledObject, transform);
            newObject.SetActive(false);
            bossPoolOfObjects.Add(newObject);
        }
    }

    public GameObject GetPooledObject() {
            foreach (GameObject v in poolOfObjects) {
                if (!v.activeInHierarchy) {
                    return v;
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
    public GameObject GetBossPooledObject() {
        foreach (GameObject v in bossPoolOfObjects) {
            if (!v.activeInHierarchy) {
                return v;
            }
        }

        if (canGrow) {
            GameObject newObject = Instantiate(bossPooledObject, transform);
            newObject.SetActive(false);
            bossPoolOfObjects.Add(newObject);
            return newObject;
        }

        return null;
    }
}
