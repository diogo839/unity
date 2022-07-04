using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoxInLava : MonoBehaviour
{
    private Animation anim;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Lava")
        {
                    
            Destroy(gameObject,10f);
            anim.Play("ExplosionAnim");
        }
    }

    

}
