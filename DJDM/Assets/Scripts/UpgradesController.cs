using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour {
    private Animator myAnimator = null;
    [SerializeField]
    private string type = null;

    private bool flag = false;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    public void OpenChest() {
        Debug.Log(GameManager.Instance.DamageMultiplier());
        if (!myAnimator.GetBool("open")) {
            myAnimator.SetBool("open", true);

            GameManager.Instance.Upgrade(type);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !flag) {
            myAnimator.SetTrigger("Open");
            GameManager.Instance.Upgrade(type);
            flag = true;
        }
    }
}
