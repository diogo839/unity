using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    [SerializeField]
    private GameObject panelHUD;
    [SerializeField]
    private GameObject panelPause;
    [SerializeField]
    private GameObject panelDead;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void ShowPanelPause(bool value) {
        panelPause.SetActive(value);
    }

    public void ShowPanelDead(bool value) {
        panelDead.SetActive(value);
    }

    public void ShowHUD(bool value) {
        panelHUD.SetActive(value);
    }
}
