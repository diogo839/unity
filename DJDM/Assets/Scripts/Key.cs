using UnityEngine;

public class Key : MonoBehaviour {
    [SerializeField]
    private AudioSource playerAudioSource = null;
    [SerializeField]
    private AudioClip getKeyClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            playerAudioSource.PlayOneShot(getKeyClip);
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<PlayerController>().hasKey = true;
        }
    }
}