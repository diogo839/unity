using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer.Equals("Wall") || other.gameObject.layer.Equals("Ground")) {
            Dismiss();
        }
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().TakeDamage();
            Dismiss();
        }
    }

    private void OnBecameInvisible() {
        Dismiss();
    }

    private void Dismiss() {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
