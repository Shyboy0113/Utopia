using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeTheBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Hidden_Trigger Ground")
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject,10);
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            
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
