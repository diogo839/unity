using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOptions : MonoBehaviour
{
    [SerializeField]
    private GameObject menuOptions;
    [SerializeField]
    private GameObject optionsButton;

    public void OptionsMenu() {
        menuOptions.SetActive(true);
        optionsButton.SetActive(false);
    }
}
