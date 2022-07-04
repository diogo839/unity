using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField]
    private float damage = 25f;
    [SerializeField]
    private PlayerController playerController;
    public AudioSource audio;
    private bool inLava = false;
    IEnumerator coroutine;

    private void Awake() {
        coroutine = TakeDamage(playerController);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            audio.Play();
            StartCoroutine(coroutine);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            audio.Stop();
            StopCoroutine(coroutine);
        }
    }
    IEnumerator TakeDamage(PlayerController player) {
        while (true) {
            yield return new WaitForFixedUpdate();
            player.TakeDamage(damage);
        }
    }
}


