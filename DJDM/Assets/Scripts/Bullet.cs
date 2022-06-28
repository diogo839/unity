using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Wall") || other.CompareTag("Platform")) {
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
