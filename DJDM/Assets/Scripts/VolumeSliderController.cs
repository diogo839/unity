using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    [SerializeField]
    private Slider[] sliders = null;

    public void ChangeVolume(float volume) {
        AudioManager.Instance.ChangeMasterVolume(volume);
        foreach (var s in sliders) {
            s.GetComponent<Slider>().value = volume;
        }
    }
}
