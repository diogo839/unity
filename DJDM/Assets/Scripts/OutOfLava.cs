using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfLava : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bridges = null;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            foreach (GameObject bridge in bridges) { 
                bridge.SetActive(true);
            }
        }
    }
}
