using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour
{
    
    //캐릭터 이동
    public CharacterController2D controller;
    public Animator animator; // 애니메이터에 대한 참조 추가
    
    public float speed = 20f;
    private float inputX = 0f;
    
    private bool jump = false;

    private bool isPlayingExhausted = false;

    private void Start()
    {
        // animator 변수에 Animator 컴포넌트에 대한 참조 가져오기
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.playerStamina <= 0f)
        {
            if(!isPlayingExhausted)
            {
                StartCoroutine(PlayExhausted());
            }
        }

        inputX = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump") && !isPlayingExhausted) jump = true;

        if (!isPlayingExhausted)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GameManager.Instance.staminaVector = -1;
                speed = 40f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                GameManager.Instance.staminaVector = 1;
                speed = 20f;
            }
        }

    }

    
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isStory && !GameManager.Instance.isExhausted)
        {
            controller.Move(inputX * Time.fixedDeltaTime, jump);
            jump = false;

            // 애니메이션 조절
            UpdateAnimator();
        }
        else
        {
            controller.Move(0f,false);
        }
    }

    private IEnumerator PlayExhausted()
    {

        //애니메이터에서 탈진 상태 구현해놔야 함
        animator.SetBool("IsExhausted", true);

        isPlayingExhausted = true;
        GameManager.Instance.isExhausted = true;

        GameManager.Instance.staminaVector = 0;
        GameManager.Instance.playerStamina = 0.001f;

        yield return new WaitForSeconds(3f);

        isPlayingExhausted = false;
        GameManager.Instance.isExhausted = false;
        GameManager.Instance.staminaVector = 1;

        animator.SetBool("IsExhausted", false);
        speed = 20f;

    }

    // 애니메이션을 조절하는 함수
    private void UpdateAnimator()
    {
        // 플레이어의 y 속도를 가져옴
        float verticalVelocity = controller.GetVerticalVelocity();
        float HorizontalVelocity = controller.GetHorizontalVelocity();
        
        bool isGrounded = controller._grounded;

        // 높은 곳에서 떨어져서 y 속도가 음수이거나, 점프를 한 뒤 y 속도가 0 이하로 떨어졌을 경우 "떨어지는 애니메이션" 실행
        bool isFalling = !isGrounded && verticalVelocity < 0;
        bool isMoving = Mathf.Abs(HorizontalVelocity) > 0.1f ;

        // "떨어지는 애니메이션" 상태를 Animator에 전달
        animator.SetBool("IsFalling", isFalling);

        // "바닥에 닿았는가" 상태를 Animator에 전달
        animator.SetBool("IsGround", isGrounded);

        // 점프 상태를 결정
        animator.SetBool("IsJumping", !isGrounded && !isFalling);
        
        animator.SetBool("IsMoving", isMoving);
        

    }

}