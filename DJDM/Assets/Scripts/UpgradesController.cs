using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradesController : MonoBehaviour {
    private Animator myAnimator = null;
    private AudioSource myAudioSource = null;
    [SerializeField]
    private string type = null;
    private bool opened = false;
    private int lvl;

    [SerializeField]
    private TMP_Text upgradeText = null;
    [SerializeField]
    private GameObject upgradePanel = null;

    [SerializeField]
    private AudioClip openAudioClip;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        myAudioSource = GetComponent<AudioSource>();
    }
    private void Start() {
        lvl = GameManager.Instance.GetLvl();
    }

    public void OpenChest() {
        if (!myAnimator.GetBool("open") && !opened && lvl == 2) {
            myAnimator.SetBool("open", true);
            GameManager.Instance.Upgrade(type);
            Debug.Log(GameManager.Instance.DamageMultiplier());
            DoSoundAndText();
            opened = true;
        }
    }

    private void DoSoundAndText() {
        myAudioSource.PlayOneShot(openAudioClip);
        upgradeText.text = "You've got a " + type + " upgrade!";
        upgradeText.enabled = true;
        upgradePanel.SetActive(true);
        StartCoroutine(UpgradeTextTime());
    }

    private IEnumerator UpgradeTextTime() {
        yield return new WaitForSeconds(1f);
        upgradeText.enabled = false;
        upgradePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !opened && lvl == 3) {
            myAnimator.SetTrigger("Open");
            GameManager.Instance.Upgrade(type);
            DoSoundAndText();
            opened = true;
        }
    }
}
