using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNum : MonoBehaviour
{
    public DoorPassword doorLock;

    public void TypeNumber(int num)
    {
        doorLock.AddNumber(num);
    }
}