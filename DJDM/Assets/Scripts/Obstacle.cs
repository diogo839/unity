using UnityEngine;

public class Obstacle : MonoBehaviour
{

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Car car = collision.gameObject.GetComponent<Car>();

        if (car)
        {
            //Toca animação de destruir, etc.
         
            Destroy(gameObject);
        }
    }
}