using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    [SerializeField]
    private float health = 1000f;
    [SerializeField]
    private Transform shootPointTransform = null;

    private Rigidbody2D rb = null;
    private Animator anim = null;

    private float shootSpeed = 2f;
    private float currentWalkSpeed = 0;
    private bool playerIn = false;
    private GameObject player = null;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        rb.velocity = new Vector2(transform.right.x * currentWalkSpeed,
                rb.velocity.y);

        anim.SetFloat("MovementSpeed", Mathf.Abs(currentWalkSpeed));
    }

    private void FixedUpdate() {
        if (playerIn) {
            Flip();
        }
        if (health <= 0) {
            StartCoroutine("WaitToDie");
        }
    }
    private void Die() {
        Destroy(gameObject);
    }


    public void TakeDamage() {
        health -= GameManager.Instance.baseDamage * GameManager.Instance.DamageMultiplier();
    }

    private void Flip() {
        Vector3 targetRotation = transform.localEulerAngles;
        if (player.transform.position.x < transform.position.x) {
            targetRotation.y = 180;
        } else {
            targetRotation.y = 0;
        }
        transform.localEulerAngles = targetRotation;

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            currentWalkSpeed = 1f;
            player = collision.gameObject;
            playerIn = true;
            StartCoroutine("WaitAndShoot");

        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            currentWalkSpeed = 0f;
            playerIn = false;
        }
    }

    private void Shoot() {
        //GameObject brick = Instantiate(projectilePrefab);
        GameObject brick = ObjectPoolingManager.Instance.GetBossPooledObject();
        brick.transform.position = shootPointTransform.position;
        brick.transform.rotation = shootPointTransform.rotation;
        brick.SetActive(true);
        brick.GetComponent<Rigidbody2D>().velocity =
            shootPointTransform.right * shootSpeed;
        StartCoroutine("WaitAndShoot");

        //play shoot audio
        //myAudioSource.PlayOneShot(shootAudioClips[Random.Range(0, shootAudioClips.Length)]);
    }
    IEnumerator WaitAndShoot() {
        yield return new WaitForSeconds(shootSpeed);
        Shoot();
    }
    IEnumerator WaitToDie() {
        anim.SetTrigger("Die");
        //currentWalkSpeed = 0;
        StopCoroutine("WaitAndShoot");
        rb.velocity = new Vector2(transform.right.x * -currentWalkSpeed,
                1f);
        yield return new WaitForSeconds(2);
        Die();
    }
}
