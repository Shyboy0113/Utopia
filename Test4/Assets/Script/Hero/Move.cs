using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController2D controller;
    
    public float speed = 40f;

    private float inputX = 0f;
    
    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal") * speed;

    }

    private void FixedUpdate()
    {
        controller.Move(inputX * Time.fixedDeltaTime, false, false);
    }
}
