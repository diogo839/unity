using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
  
    public static DialogueController instance;
    public GameObject dialoguePanel, imagebg;
    public Button nextButton;
    public TextMeshProUGUI dialogueText; 
    public TextMeshProUGUI dialogueSize; 
    public DialogueAgent currentNPC;
    private bool typing;

    private void Awake() => instance = this;

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        imagebg.SetActive(true);

        if (typing)
        {
            StopCoroutine(Type());
        }

        StartCoroutine(Type());
    }

    public void NextSentence()
    {
        if (currentNPC.currentSentence < currentNPC.sentences.Length - 1)
        {
            currentNPC.currentSentence++;

            if (typing)
            {
                StopCoroutine(Type());
            }

            StartCoroutine(Type());
        }
    }

    public void StopDialogue()
    {
        dialoguePanel.SetActive(false);
        imagebg.SetActive(false);
        StopCoroutine(Type());
    }

    #region TYPE

    WaitForSeconds wait = new WaitForSeconds(0.1f);

    private IEnumerator Type()
    {
        typing = true;

        string sentence = currentNPC.sentences[currentNPC.currentSentence];
        dialogueSize.text = sentence;//Ajusta o tamanho do painel 
        nextButton.gameObject.SetActive(false);
        int index = 0;
        string temp = string.Empty;

        while (index < sentence.Length)
        {
            temp += sentence[index];
            index++;
            dialogueText.text = temp;
            yield return wait;
        }

        nextButton.gameObject.SetActive(true);
        typing = false;
    }

    #endregion
}