using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour {


    private Animator animator = null;
    private int attackHashAnimation = 1080829965; // tag hash for attack state in animator
    public bool attacking = false;
    IEnumerator chestCoroutine;
    IEnumerator enemyCoroutine;
    private void Awake() {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            enemyCoroutine = CheckDamageEnemy(collision.GetComponent<EnemyController>());
            StartCoroutine(enemyCoroutine);
        } else if (collision.CompareTag("Chest")) {
            chestCoroutine = CheckOpenChest(collision.GetComponent<UpgradesController>());
            StartCoroutine(chestCoroutine);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            StopCoroutine(enemyCoroutine);
        } else if (collision.CompareTag("Chest")) {
            StopCoroutine(chestCoroutine);
        }
    }

    IEnumerator CheckOpenChest(UpgradesController upgrades) {
        while (true) {
            yield return new WaitForFixedUpdate();
            if (animator.GetCurrentAnimatorStateInfo(0).tagHash == attackHashAnimation && attacking) {
                upgrades.OpenChest();
                attacking = false;
            }
        }
    }
    IEnumerator CheckDamageEnemy(EnemyController enemy) {
        while (true) {
            yield return new WaitForFixedUpdate();
            if (animator.GetCurrentAnimatorStateInfo(0).tagHash == attackHashAnimation && attacking) {
                enemy.TakeDamage();
                attacking = false;
            }
        }
    }
}
