using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStarter : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance._playerHp = 5;
    }
}
