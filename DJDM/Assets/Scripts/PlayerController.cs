using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float jumpForce = 300f;
    [SerializeField]
    private Transform[] feetTransform = null;
    [SerializeField]
    private LayerMask groundLayerMask = 128;
    [Header("Shoot")]
    [SerializeField]
    private Transform shootPointTransform = null;
    [SerializeField]
    private float shootSpeed = 6f;

    private Rigidbody2D myRigidbody = null;
    private Animator myAnimator = null;

    private float moveDirection = 0f;
    private bool jump = false;
    private bool shoot = false;

    private Collider2D[] groundCheckColliders = new Collider2D[1];
    private bool onGround = false;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update() {
        moveDirection = SimpleInput.GetAxis("Horizontal");

        if (CheckForFlip()) {
            Flip();
        }

        if (!jump) {
            jump = SimpleInput.GetButtonDown("Jump");
        }

        myRigidbody.velocity = new Vector2(moveDirection * walkSpeed,
            myRigidbody.velocity.y);
        myAnimator.SetFloat("HorizontalVelocity",
                Mathf.Abs(myRigidbody.velocity.x));
    }
    private void FixedUpdate() {

        onGround = CheckForGround();
        if (jump && onGround) {
            Jump();
        }
        if (shoot) {
            Shoot();
        }

        jump = false;
        shoot = false;
    }

    private bool CheckForFlip() {
        return (transform.right.x > 0 && moveDirection < 0) ||
            (transform.right.x < 0 && moveDirection > 0);
    }

    private bool CheckForGround() {

        for (int i = 0; i < feetTransform.Length; i++) {
            if (Physics2D.OverlapPointNonAlloc(
                feetTransform[i].position,
                groundCheckColliders,
                groundLayerMask) > 0) {
                return true;
            }
        }
        return false;
    }

    private void Flip() {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180f;
        transform.localEulerAngles = targetRotation;
    }

    private void Jump() {
        myRigidbody.velocity = new Vector2(
            myRigidbody.velocity.x, 0);
        myRigidbody.AddForce(Vector2.up * jumpForce);
    }

    private void Shoot() {
        //GameObject brick = Instantiate(projectilePrefab);
        GameObject gem = ObjectPoolingManager.Instance.GetPooledObject();
        gem.transform.position = shootPointTransform.position;
        gem.transform.rotation = shootPointTransform.rotation;
        gem.SetActive(true);
        gem.GetComponent<Rigidbody2D>().velocity =
            shootPointTransform.right * shootSpeed;

        SmoothFollow.Instance.Shake(0.1f, 0.05f);
    }
}
