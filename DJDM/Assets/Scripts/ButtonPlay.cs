using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlay : MonoBehaviour
{
    public GameObject PauseMenu;
    public void StartLevel() {
        GameManager.Instance.LoadNextLevel();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void OptionsButton()
    {
        PauseMenu.SetActive(true);
    }


}
