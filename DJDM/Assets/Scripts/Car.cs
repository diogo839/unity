using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float carSpeed, maxTime = 30;
    private bool canUse = true;
    private bool startMoving = false;
    private bool playerInCar;
    private GameObject playerReal;
    public GameObject playerVisual;
   

    public GameObject infoText; //You need a key?
    public Rigidbody2D myRigidbody;
    public AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        infoText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canUse)
        {
            return;//caso ele saia do carro, pra não aparecer a mensagem de key novamente
        }
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (!player)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (player.hasKey)
            {
               
                player.hasKey = false;
                playerVisual.SetActive(true);
                playerReal = player.gameObject; //Armazena o player real pra ativar quando sair;
                player.gameObject.SetActive(false); //Desativa o player real                
                playerInCar = true;
                SmoothFollow.Instance.SetTarget(this.transform);
                audio.Play();
            } else if (!player.hasKey)
            {
                Debug.Log("Sem chave");
                infoText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        infoText.SetActive(false);
    }

    private void Update()
    {
        MoveCar();
    }

    public void MoveCar()
    {
        if (!playerInCar) return;

        if (!startMoving) // 30s a partir do momento que apertei pra andar.
        {
            startMoving = true;
            Invoke(nameof(ExitVehicle), maxTime); //Após 30s o player é forcado a sair do carro;
        }

        float moveDirection = Mathf.Clamp01(SimpleInput.GetAxis("Horizontal")); 
       

        if (moveDirection > 0.1f)
        {
            myRigidbody.velocity = new Vector2(moveDirection * carSpeed * GameManager.Instance.SpeedMultiplier(), myRigidbody.velocity.y);
        }
    }

    public void ExitVehicle() 
    {
        
        canUse = false;
        playerVisual.SetActive(false);       
        playerReal.transform.position = transform.position; 
        playerReal.SetActive(true); 
        
        SmoothFollow.Instance.SetTarget(playerReal.transform);
        enabled = false;
        audio.Stop();
    }
}
