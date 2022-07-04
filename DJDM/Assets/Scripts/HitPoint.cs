using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{


    private Animator animator = null;
    private int attackHashAnimation = 1080829965; // tag hash for attack state in animator
    public bool attacking = false;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(attacking);
        if (collision.CompareTag("Enemy") && animator.GetCurrentAnimatorStateInfo(0).tagHash == attackHashAnimation && attacking)
        {
            collision.GetComponent<EnemyController>().TakeDamage();
            attacking = false;
        }
        else if (collision.CompareTag("chest") && animator.GetCurrentAnimatorStateInfo(0).tagHash == attackHashAnimation && attacking) { 
        
            collision.GetComponent<UpgradesController>().OpenChest();
            attacking = false;
        }

    }
}
