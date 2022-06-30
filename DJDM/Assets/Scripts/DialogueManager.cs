using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] sentences;
    public float textSpeed;
    private int index;
    public GameObject imagebg;

    public GameObject button;

    private void Start()
    {
        button.SetActive(false);
        imagebg.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.SetActive(true);
            imagebg.SetActive(true);
            print("Ola");
            StartCoroutine(Type());


        }
        
        

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        button.SetActive(false);
        imagebg.SetActive(false);
        StopCoroutine(Type());
    }



    IEnumerator Type()
    {
        foreach (char c in sentences[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextSentences()
    {
        if (index<sentences.Length-1)
        {
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            
            text.text = "";
            
            button.SetActive(false);
            imagebg.SetActive(false);
        }
    }


}
