using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject playButton;
    public GameObject exitButton;
    public GameObject menuOptions;
    public GameObject backButton;
    public GameObject optionsButton;

    private void Start()
    {
        menuOptions.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
      Application.Quit();
        print("A sair do jogo");
    }

    public void OptionsMenu()
    {
        menuOptions.SetActive(true);
        optionsButton.SetActive(false);
       
        
    }

    public void BackToMenu()
    {
        menuOptions.SetActive(false);
        optionsButton.SetActive(true);
    }
}
