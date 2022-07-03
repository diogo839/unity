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
    private LayerMask hitLayerMask = 128;
    [SerializeField]
    private float initialLife = 150f;
    [Header("Shoot")]
    [SerializeField]
    private Transform shootPointTransform = null;
    [SerializeField]
    private Transform hitPointTransform = null;
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
    public Animator myAnimator = null;

    private float moveDirection = 0f;

    private bool jump = false;
    private Collider2D[] groundCheckColliders = new Collider2D[1];
    private Collider2D[] hitCheckCollider = new Collider2D[1];
    private bool onGround = false;

    private float life = 100f;
    private bool isAlive = true;
    private bool shoot = false;
    public bool hasKey;

    private bool hit = false;



    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();

        life = initialLife;
    }

    private void Start() {
        UpdateLifebar();
    }

    private void Update() {
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

            if (!hit) {
                hit = SimpleInput.GetButtonDown("Fire1");
            }

            if (!shoot) {
                shoot = SimpleInput.GetButtonDown("Shoot");
            }

            myRigidbody.velocity = new Vector2(moveDirection * walkSpeed * GameManager.Instance.SpeedMultiplier(),
                myRigidbody.velocity.y);
            myAnimator.SetFloat("HorizontalVelocity",
                Mathf.Abs(myRigidbody.velocity.x));
        }
    }

    private int jumps = 0;
    private void FixedUpdate() {

        onGround = CheckForGround();
        if (onGround) {
            jumps = 0;
            myAnimator.SetBool("Jump", false);
            myAnimator.SetBool("DoubleJump", false);
        }
        if (jump) {
            if (onGround) {
                jumps = 1;
                Jump();
            } else if (jumps < 2 && GameManager.Instance.CanDoubleJump()) {
                jumps = 2;
                Jump();
                myAnimator.SetBool("DoubleJump", true);
            }
        }
        if (hit && !jump) {
            myAnimator.SetTrigger("Attack2");
            GetComponentInChildren<HitPoint>().attacking = true;
        }

        if (shoot && GameManager.Instance.CanShoot()) {
            Shoot();
        }

        jump = false;
        shoot = false;
        hit = false;
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
        //play jump audio
        //myAudioSource.PlayOneShot(jumpAudioClip);

        myRigidbody.velocity = new Vector2(
            myRigidbody.velocity.x, 0);
        myRigidbody.AddForce(Vector2.up * jumpForce * GameManager.Instance.JumpMultiplier());
        myAnimator.SetBool("Jump", true);
    }

    private void UpdateLifebar() {
        lifebarImage.fillAmount = life / initialLife;
    }

    public void TakeDamage(float damage) {
        if (isAlive) {
            life -= damage;

            if (life < 0) {
                life = 0;
            }

            UpdateLifebar();
            myAnimator.SetBool("Damage", true);

            if (life == 0) {
                isAlive = false;
                Die();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Spikes")) {
            myAnimator.SetBool("Damage", false);
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Hadouken")) {
            StartCoroutine("Wait");
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void Shoot() {
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
    IEnumerator Wait() {
        yield return new WaitForSeconds(0.5f);
        myAnimator.SetBool("Damage", false);
    }
}
