using UnityEngine;

public class DialogueAgent : MonoBehaviour
{
    //Este é o script que fica em cada NPC, ele armazena apenas os dados da conversa.
    public string[] sentences;
    public int currentSentence;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueController.instance.currentNPC = this;
            DialogueController.instance.StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueController.instance.StopDialogue();
            DialogueController.instance.currentNPC = null;
        }
    }
}