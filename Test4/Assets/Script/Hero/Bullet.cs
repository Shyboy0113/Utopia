using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _attackPower;
    private string _attribute;

    public void SetBulletProperties(int attackPower, string attribute)
    {
        _attackPower = attackPower;
        _attribute = attribute;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("적에게 " + _attackPower +" 데미지가 들어갔습니다.");
            
            Destroy(this);
        }
        
    }


}
