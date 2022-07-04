using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            Dismiss();
        }
        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().TakeDamage();
            Dismiss();
        }
        if (other.CompareTag("Boss")) {
            other.GetComponent<BossController>().TakeDamage();
            Dismiss();
        }
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().TakeDamage(GameManager.Instance.bossBaseDamage);
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
