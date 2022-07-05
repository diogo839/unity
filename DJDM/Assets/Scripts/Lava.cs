using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField]
    private float damage = 25f;
    [SerializeField]
    private PlayerController playerController;
    IEnumerator coroutine;

    [SerializeField]
    private AudioSource playerAudioSource = null;
    [SerializeField]
    private AudioClip screamAudioClip;

    private void Awake() {
        coroutine = TakeDamage(playerController);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(coroutine);
            playerAudioSource.clip = screamAudioClip;
            playerAudioSource.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StopCoroutine(coroutine);
            playerAudioSource.Stop();
            playerAudioSource.clip = null;
        }
    }
    IEnumerator TakeDamage(PlayerController player) {
        while (true) {
            yield return new WaitForFixedUpdate();
            player.TakeLavaDamage(damage);
        }
    }
}


