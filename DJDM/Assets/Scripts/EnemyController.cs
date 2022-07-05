using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
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
    private float initialHealth = 100f;
    [SerializeField]
    private float health = 100f;

    [SerializeField]
    private Image lifebarImage = null;


    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        currentWalkSpeed = walkSpeed;
        health = initialHealth;
        UpdateLifebar();
    }

    private void Update() {
        myRigidbody.velocity = new Vector2(transform.right.x * walkSpeed,
                myRigidbody.velocity.y);
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
        UpdateLifebar();
    }

    private void UpdateLifebar() {
        lifebarImage.fillAmount = health / initialHealth;
    }

    private void Flip() {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180f;
        transform.localEulerAngles = targetRotation;

        Vector3 lifebarTargetRotation = lifebarImage.transform.localEulerAngles;
        lifebarTargetRotation.y += 180;
        lifebarImage.transform.localEulerAngles = lifebarTargetRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().TakeDamage(baseDamage);
        }
    }
}
