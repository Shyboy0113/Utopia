using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Move : MonoBehaviour
{
    public CharacterController2D controller;
    private Animator _animator;
    
    public float speed = 40f;
    private float inputX = 0f;
    
    private bool jump = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            _animator.Play("Player_Fall");
               
        }
        
    }

    private void FixedUpdate()
    {
        controller.Move(inputX * Time.fixedDeltaTime,jump);
        jump = false;
        
    }
}
