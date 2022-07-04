using TMPro;
using UnityEngine;

public class DoorPassword : MonoBehaviour
{
    

    public bool isOpen = false;
    public TMP_Text textInput;
    public string typedPassword;
    public string correctPassword;
    public GameObject painel;
    private int maxAttempts = 3;//numero tentativas maximas parar inserir código
    public AudioSource sound, sound2;


   
    public GameObject collider;

    public void Start()
    {
       
        collider.SetActive(true);
    }

    public void AddNumber(int num)
    {
        typedPassword += num.ToString();
        textInput.text = typedPassword;
    }

    //butao X
    public void CancelInput()
    {
        typedPassword = string.Empty;
        textInput.text = string.Empty;
    }

    

    //butao OK
    public void CheckTypedCode()
    {
        if (typedPassword == correctPassword)
        {
            sound2.Play();
            painel.SetActive(false);
            collider.SetActive(false);
          
         
        }
        if (typedPassword != correctPassword)
        {
            //  maxAttempts = maxAttempts - 1;
            sound.Play();
          
            
        }
        if (maxAttempts == 0)
        {
          
           
        }
    }


   
}
