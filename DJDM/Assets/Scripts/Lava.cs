using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField]
    private float damage = 25f;
    public AudioSource audio;



    private void OnCollisionStay2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (other.gameObject.tag == "Player")
        {
            audio.Play();
            player.TakeDamage(damage);

        }
    }

    
}


