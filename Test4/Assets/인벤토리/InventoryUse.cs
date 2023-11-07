using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUse : MonoBehaviour
{
    void Update () {
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeyDown_1();
        }

    }
    private void KeyDown_1()
    {
        if(GameManager.Instance._playerHp >= GameManager.Instance.MaxHp)
        {
            Debug.Log("이미 더 마실 필요가 없을 것 같다.");
        }
        
        Debug.Log("붉은 물약을 사용하여 Hp를 1 회복하였다.");
        GameManager.Instance._playerHp += 1;
    }
    
    
}
