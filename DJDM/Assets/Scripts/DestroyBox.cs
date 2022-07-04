using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour {
    public GameObject key;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            key.transform.position = transform.position;
            Destroy(gameObject);
            key.SetActive(true);
        }
    }

}
