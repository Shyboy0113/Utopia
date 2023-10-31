using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;


public class MoveCharacter : MonoBehaviour
{
    
    //캐릭터 이동
    public CharacterController2D controller;
    public Animator animator; // 애니메이터에 대한 참조 추가
    
    public float speed = 40f;
    private float inputX = 0f;
    
    private bool jump = false;
    public static bool crouch = false;
    public static bool isGroggy = false;
    
    private bool isGuard = false;

    private void Start()
    {
        // animator 변수에 Animator 컴포넌트에 대한 참조 가져오기
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isGroggy && !GameManager.Instance.isStory)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // Assuming a guard posture
                GameManager.Instance.OnGuardPostureActivated.Invoke();
                isGuard = true;

            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                // Exiting guard posture
                GameManager.Instance.OnGuardPostureDeactivated.Invoke();
                isGuard = false;
            }

            inputX = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButtonDown("Jump")) jump = true;

            if (Input.GetButton("Crouch"))
            {
                // Only set crouch to true if the character is grounded
                if (controller.m_Grounded)
                {
                    crouch = true;
                }
            }
            else
            {
                if (controller.IsReachedCeiling() is false)
                {
                    crouch = false;
                }
                else
                {
                    crouch = true;
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (!isGroggy)
        {
            controller.Move(inputX * Time.fixedDeltaTime, crouch, jump);
            jump = false;
            // 애니메이션 조절
            UpdateAnimator();
        }
    }

    public IEnumerator StartGroggy(float duration)
    {
        isGroggy = true;
        isGuard = false;
        GameManager.Instance.OnGuardPostureDeactivated.Invoke();
        
        animator.SetBool("IsGroggy", true);
        animator.SetBool("IsGuard", false);

        yield return new WaitForSeconds(duration);

        // After the specified duration, set isGroggy to false and play idle animation
        
        animator.SetBool("IsGroggy", false);
        isGroggy = false;
        yield return null;
        
    }

    // 애니메이션을 조절하는 함수
    private void UpdateAnimator()
    {
        // 플레이어의 y 속도를 가져옴
        float verticalVelocity = controller.GetVerticalVelocity();
        float HorizontalVelocity = controller.GetHorizontalVelocity();
        
        bool isGrounded = controller.m_Grounded;

        // 높은 곳에서 떨어져서 y 속도가 음수이거나, 점프를 한 뒤 y 속도가 0 이하로 떨어졌을 경우 "떨어지는 애니메이션" 실행
        bool isFalling = !isGrounded && verticalVelocity < 0;
        bool isMoving = Mathf.Abs(HorizontalVelocity) > 0.1f ;

        // "떨어지는 애니메이션" 상태를 Animator에 전달
        animator.SetBool("IsFalling", isFalling);

        // "바닥에 닿았는가" 상태를 Animator에 전달
        animator.SetBool("IsGround", isGrounded);

        // 점프 상태를 결정
        animator.SetBool("IsJumping", !isGrounded && !isFalling);
        
        animator.SetBool("IsMoving", !crouch && isMoving);
        
        // 웅크리는 상태를 Animator에 전달
        animator.SetBool("IsCrouching", crouch);
        animator.SetBool("IsCrouchMoving", crouch && isMoving);
        
        animator.SetBool("IsGuard", isGuard);

    }

}