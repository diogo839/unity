using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string correctKey;
    public GameObject collider;
    public GameObject panel;

    void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.gameObject.tag == "Player")
        {
            DoorPassword door = GetComponent<DoorPassword>();
            panel.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        panel.SetActive(false);
    }



}

