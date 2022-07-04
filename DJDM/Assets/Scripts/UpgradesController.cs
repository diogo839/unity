using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour {
    private Animator myAnimator = null;
    [SerializeField]
    private string type = null;
    private bool opened = false;
    private int lvl;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }
    private void Start() {
        lvl = GameManager.Instance.GetLvl();
    }

    public void OpenChest() {
        if (!myAnimator.GetBool("open") && !opened && lvl == 2) {
            myAnimator.SetBool("open", true);
            GameManager.Instance.Upgrade(type);
            Debug.Log(GameManager.Instance.DamageMultiplier());

            opened = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !opened && lvl == 3) {
            myAnimator.SetTrigger("Open");
            GameManager.Instance.Upgrade(type);
            opened = true;
        }
    }
}
