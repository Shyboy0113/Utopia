using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeTheBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("벽돌이 충돌했습니다.");
        
        if(col.gameObject.name == "Hidden_Trigger Ground")
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject,10);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("게임 오버!!");
        }
        else
        {
            GameObject _gameObject = GameObject.Find("Hidden_Trigger Ground");
            if (_gameObject is null)
            {
                Destroy(this.gameObject, 10);
            }
        }
    }
}
