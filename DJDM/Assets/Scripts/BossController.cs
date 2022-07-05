using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour {
    [SerializeField]
    private float initialHealth = 1000f;
    [SerializeField]
    private float health = 1000f;
    [SerializeField]
    private Transform shootPointTransform = null;
    [SerializeField]
    private Transform cameraTransform = null;
    [SerializeField]
    private float shootSpeed = 2f;
    [SerializeField]
    private float chargeAttackSpeed = 5f;
    [SerializeField]
    private float attackSpeed = 2f;

    [SerializeField]
    private Image lifebarImage = null;

    private Rigidbody2D rb = null;
    private Animator anim = null;

    private float currentWalkSpeed = 0;
    private bool playerIn = false;
    private GameObject player = null;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        health = initialHealth;
        UpdateLifebar();
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
        GetComponentInChildren<EndLevel>().End();
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
        Vector3 oldRotation = targetRotation;
        if (player.transform.position.x < transform.position.x) {
            targetRotation.y = 180;
        } else {
            targetRotation.y = 0;
        }
        transform.localEulerAngles = targetRotation;
        if(oldRotation.y != targetRotation.y) {
            Vector3 lifebarTargetRotation = lifebarImage.transform.localEulerAngles;
            lifebarTargetRotation.y += 180;
            lifebarImage.transform.localEulerAngles = lifebarTargetRotation;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            currentWalkSpeed = 1f;
            player = collision.gameObject;
            playerIn = true;
            InvokeRepeating(nameof(Shoot), chargeAttackSpeed, chargeAttackSpeed);
            InvokeRepeating(nameof(Shoot), attackSpeed, attackSpeed);
            Camera.main.orthographicSize = 5;
            SmoothFollow.Instance.SetTarget(cameraTransform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            currentWalkSpeed = 0f;
            playerIn = false;
            Camera.main.orthographicSize = 3;
            SmoothFollow.Instance.SetTarget(collision.gameObject.transform);
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

        //play shoot audio
        //myAudioSource.PlayOneShot(shootAudioClips[Random.Range(0, shootAudioClips.Length)]);
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
