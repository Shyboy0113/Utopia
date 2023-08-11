using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController2D controller;
    
    public float speed = 40f;
    private float inputX = 0f;
    
    private bool jump = false;
    private bool crouch = false;
    
    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump")) jump = true;
        if (Input.GetButtonDown("Crouch")) crouch = true;
        else if(Input.GetButtonUp("Crouch")) crouch = false;
        
    }

    private void FixedUpdate()
    {
        controller.Move(inputX * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
}
