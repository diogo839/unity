using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRespawn : MonoBehaviour
{
    public void Respawn() {
        GameManager.Instance.ReloadLevel();
    }
}
