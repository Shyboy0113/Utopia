using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Wall : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        Animator animator = col.gameObject.GetComponent<Animator>();
    }
}



    
    


