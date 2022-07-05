using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMainMenu : MonoBehaviour
{
    public void MainMenu() {
        GameManager.Instance.LoadMainMenu();
    }
}
