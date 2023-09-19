using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Move : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator; // 애니메이터에 대한 참조 추가
    
    public float speed = 40f;
    private float inputX = 0f;
    
    private bool jump = false;
    private bool crouch = false;

    private void Start()
    {
        // animator 변수에 Animator 컴포넌트에 대한 참조 가져오기
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump")) jump = true;
        
        if (Input.GetButtonDown("Crouch")) crouch = true;
        else if(Input.GetButtonUp("Crouch")) crouch = false;

        // 애니메이션 조절
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        controller.Move(inputX * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    // 애니메이션을 조절하는 함수
    private void UpdateAnimator()
    {
        // 플레이어의 y 속도를 가져옴
        float verticalVelocity = controller.GetVerticalVelocity();
        
        bool isGrounded = controller.m_Grounded;

        // 높은 곳에서 떨어져서 y 속도가 음수이거나, 점프를 한 뒤 y 속도가 0 이하로 떨어졌을 경우 "떨어지는 애니메이션" 실행
        bool isFalling = !isGrounded && verticalVelocity < 0;

        // "떨어지는 애니메이션" 상태를 Animator에 전달
        animator.SetBool("IsFalling", isFalling);

        // "바닥에 닿았는가" 상태를 Animator에 전달
        animator.SetBool("IsGround", isGrounded);

        // 점프 상태를 결정
        animator.SetBool("IsJumping", !isGrounded && !isFalling);

        // 웅크리는 상태를 Animator에 전달
        animator.SetBool("IsCrouching", crouch);
    }

}