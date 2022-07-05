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
    public AudioSource sound;


   
    public GameObject collider;
    [SerializeField]
    private GameObject nextLevelGameObject = null;

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
            painel.SetActive(false);
            collider.SetActive(false);
            nextLevelGameObject.layer = LayerMask.NameToLayer("Ground");
        }
        if (typedPassword != correctPassword)
        {
            sound.Play();
        }
    }


   
}
