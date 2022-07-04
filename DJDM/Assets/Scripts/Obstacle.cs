using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Car car = collision.gameObject.GetComponent<Car>();

        if (car)
        {
            //Toca animação de destruir, etc.
            // anim.Play("ExplosionAnim");

            // StartCoroutine("CheckBox");
            Destroy(gameObject);
        }
    }


    //IEnumerator CheckBox()
    //{
    //    yield return new WaitForSeconds(10);

    //    anim.Play("ExplosionAnim");
    //    Destroy(gameObject, 1f);
    //}
}