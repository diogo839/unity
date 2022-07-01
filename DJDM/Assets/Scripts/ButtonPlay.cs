using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlay : MonoBehaviour
{
    public void LoadLevel() {
        GameManager.Instance.LoadNextLevel();
    }
}
