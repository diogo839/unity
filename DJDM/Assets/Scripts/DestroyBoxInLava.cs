using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoxInLava : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Lava")
        {

            StartCoroutine("CheckLava");           

        }
    }

    IEnumerator CheckLava()
    {
        yield return new WaitForSeconds(10);
       
        anim.Play("ExplosionAnim");
        Destroy(gameObject,1f);
    }

    

}
