using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed = -1f;
    private void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
        if (transform.position.x < -52.26f)
        {
            transform.position = new Vector3(52.26f, transform.position.y);
        }
    }
}
