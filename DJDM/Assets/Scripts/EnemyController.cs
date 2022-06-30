using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private Transform turningPointTransform = null;
    [SerializeField]
    private LayerMask obstacleLayerMask = 0;

    [SerializeField]
    private float baseDamage = 10f;

    private Rigidbody2D myRigidbody = null;
    private Animator myAnimator = null;

    private Collider2D[] obstacleCheckColliders = new Collider2D[1];

    private float currentWalkSpeed = 0;

    [SerializeField]
    private float health = 100f;

    private void Start() {
        //StartCoroutine(StartMovement());
    }

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        currentWalkSpeed = walkSpeed;
    }

    private void Update() {
        myRigidbody.velocity = new Vector2(transform.right.x * walkSpeed,
                myRigidbody.velocity.y);

        myAnimator.SetFloat("MovementSpeed", Mathf.Abs(currentWalkSpeed));
    }

    private void FixedUpdate() {
        if (CheckForObstacle()) {
            Flip();
        }
        if (health <= 0) {
            Die();
        }
    }
    private void Die() {
        Destroy(gameObject);
    }

    /*private IEnumerator StartMovement() {
        yield return new WaitForSeconds(3f);
        currentWalkSpeed = walkSpeed;
    }*/

    private bool CheckForObstacle() {
        if (Physics2D.OverlapPointNonAlloc(
            turningPointTransform.position,
            obstacleCheckColliders,
            obstacleLayerMask) > 0) {
            return true;
        }
        return false;
    }

    public void TakeDamage() {
        health -= GameManager.Instance.baseDamage * GameManager.Instance.DamageMultiplier();
    }

    private void Flip() {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180f;
        transform.localEulerAngles = targetRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            collision.collider.GetComponent<PlayerController>().TakeDamage(baseDamage);
        }
    }
}
