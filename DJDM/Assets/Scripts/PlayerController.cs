using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float walkSpeed = 3f;
    [SerializeField]
    private float jumpForce = 400f;
    [SerializeField]
    private Transform[] feetTransform = null;
    [SerializeField]
    private LayerMask groundLayerMask = 128;
    [SerializeField]
    private float initialLife = 150f;
    [Header("Shoot")]
    [SerializeField]
    private Transform shootPointTransform = null;
    [SerializeField]
    private float shootSpeed = 6f;

    [Header("UI")]
    [SerializeField]
    private Image lifebarImage = null;

    [Header("Audio")]
    [SerializeField]
    private AudioClip jumpAudioClip;
    [SerializeField]
    private AudioClip[] shootAudioClips;
    
    private AudioSource myAudioSource;
    private Rigidbody2D myRigidbody = null;
    private Animator myAnimator = null;

    private float moveDirection = 0f;

    private bool jump = false;
    private Collider2D[] groundCheckColliders = new Collider2D[1];
    private bool onGround = false;

    private float life = 100f;
    private bool isAlive = true;
    private bool shoot = false;

    private void Awake () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();

        life = initialLife;
    }

    private void Start () {
        UpdateLifebar();
    }

    private void Update () {
        if (GameManager.Instance.IsPaused) {
            return;
        }

        if (isAlive) {
            moveDirection = SimpleInput.GetAxis("Horizontal");

            if (CheckForFlip()) {
                Flip();
            }

            if (!jump) {
                jump = SimpleInput.GetButtonDown("Jump");
            }

            if (!shoot) {
                shoot = SimpleInput.GetButtonDown("Shoot");
            }

            myRigidbody.velocity = new Vector2(moveDirection * walkSpeed * GameManager.Instance.SpeedMultiplier(),
                myRigidbody.velocity.y);
            myAnimator.SetFloat("HorizontalVelocity",
                Mathf.Abs(myRigidbody.velocity.x));

            /*
            * DEBUG
            */

            if (Input.GetKeyDown(KeyCode.K)) {
                TakeDamage(25f);
            }
        }
    }

    private int jumps = 0;
    private void FixedUpdate () {

        onGround = CheckForGround();
        if (onGround) {
            jumps = 0;
        }
        // if (jump && onGround) {
        //     Jump();
        // }
        if (jump) {
            if (onGround) {
                jumps = 1;
                Jump();
            } else if (jumps < 2 && GameManager.Instance.CanDoubleJump()) {
                jumps = 2;
                Jump();
            }
        }

        if (shoot && GameManager.Instance.CanShoot()) {
            Shoot();
        }

        jump = false;
        shoot = false;
    }


    private bool CheckForFlip () {
        return (transform.right.x > 0 && moveDirection < 0) ||
            (transform.right.x < 0 && moveDirection > 0);
    }

    private bool CheckForGround () {
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


    private void Flip () {
        Vector3 targetRotation = transform.localEulerAngles;
        targetRotation.y += 180f;
        transform.localEulerAngles = targetRotation;
    }
    
    private void Jump () {
        //play jump audio
        //myAudioSource.PlayOneShot(jumpAudioClip);

        myRigidbody.velocity = new Vector2(
            myRigidbody.velocity.x, 0);
        myRigidbody.AddForce(Vector2.up * jumpForce * GameManager.Instance.JumpMultiplier());
    }

    private void UpdateLifebar () {
        //lifebarImage.fillAmount = life / initialLife;
    }

    public void TakeDamage (float damage) {
        if (isAlive) {
            life -= damage;

            if (life < 0) {
                life = 0;
            }

            UpdateLifebar();

            if (life == 0) {
                isAlive = false;
                Die();
            }
        }
    }

    private void Die () {
        Destroy(gameObject);
    }

    private void Shoot () {
        //GameObject brick = Instantiate(projectilePrefab);
        GameObject brick = ObjectPoolingManager.Instance.GetPooledObject();
        brick.transform.position = shootPointTransform.position;
        brick.transform.rotation = shootPointTransform.rotation;
        brick.SetActive(true);
        brick.GetComponent<Rigidbody2D>().velocity =
            shootPointTransform.right * shootSpeed;

        //play shoot audio
        //myAudioSource.PlayOneShot(shootAudioClips[Random.Range(0, shootAudioClips.Length)]);
    }
}
