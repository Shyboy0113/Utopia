using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoveCharacter : MonoBehaviour
{
    public CharacterController2D controller;
    public float speed = 40f;

    private Animator animator;
    private float moveInput;
    private bool jump;
    private bool isCrouch;
    private bool isGuard;
    private bool isGroggy;

    private void Start() => animator = GetComponent<Animator>();

    private void Update()
    {
        if (GameManager.Instance.IsStory || isGroggy) return;
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (isGroggy)
        {
            controller.Move(0f, false, false);
            return;
        }

        controller.Move(moveInput * Time.fixedDeltaTime, isCrouch, jump);
        jump = false;
        UpdateAnimator();
    }

    private void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameManager.Instance.OnGuardPostureActivated.Invoke();
            isGuard = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            GameManager.Instance.OnGuardPostureDeactivated.Invoke();
            isGuard = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            jump = true;

        if (Input.GetKey(KeyCode.DownArrow))
            isCrouch = controller.IsGrounded();
        else
            isCrouch = controller.IsCeilingReached();
    }

    public IEnumerator StartGroggy(float duration)
    {
        isGroggy = true;
        isGuard = false;
        GameManager.Instance.OnGuardPostureDeactivated.Invoke();

        animator.SetBool("IsGroggy", true);
        animator.SetBool("IsGuard", false);

        yield return new WaitForSeconds(duration);

        animator.SetBool("IsGroggy", false);
        isGroggy = false;
    }

    private void UpdateAnimator()
    {
        float vVel = controller.GetVerticalVelocity();
        float hVel = controller.GetHorizontalVelocity();
        bool grounded = controller.IsGrounded();
        bool isMoving = Mathf.Abs(hVel) > 0.1f;
        bool isFalling = !grounded && vVel < 0;
        bool isJumping = !grounded && !isFalling;

        animator.SetBool("IsFalling", isFalling);
        animator.SetBool("IsGround", grounded);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsMoving", !isCrouch && isMoving);
        animator.SetBool("IsCrouching", isCrouch);
        animator.SetBool("IsCrouchMoving", isCrouch && isMoving);
        animator.SetBool("IsGuard", isGuard);
    }

    private void OnDestroy()
    {
        isGroggy = false;
        isCrouch = false;
    }
}
