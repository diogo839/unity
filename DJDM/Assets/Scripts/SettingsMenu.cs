using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject menuOptions;
    [SerializeField]
    private GameObject optionsButton;

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume()
    {
        AudioListener.volume = slider.value; 
    }

    public void BackToMenu() {
        menuOptions.SetActive(false);
        optionsButton.SetActive(true);
    }
}
