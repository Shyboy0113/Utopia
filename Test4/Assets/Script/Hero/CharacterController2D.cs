using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float crouchSpeedMultiplier = 0.36f;
    [SerializeField, Range(0f, 0.3f)] private float movementSmoothing = 0.05f;
    [SerializeField] private bool airControl = true;

    [Header("Physics Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Collider2D crouchDisableCollider;

    private const float GroundedRadius = 0.2f;
    private const float CeilingRadius = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    private bool grounded;
    private bool wasCrouching;
    private float jumpMultiplier = 1.5f;

    [Header("Events")]
    public UnityEvent OnLandEvent = new();
    public UnityEvent<bool> OnCrouchEvent = new();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnGuardPostureActivated.AddListener(() => jumpMultiplier = 2f);
        GameManager.Instance.OnGuardPostureDeactivated.AddListener(() => jumpMultiplier = 1.5f);
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = Physics2D.OverlapCircle(groundCheck.position, GroundedRadius, whatIsGround);

        if (grounded && !wasGrounded)
            OnLandEvent.Invoke();
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!grounded && !airControl) return;

        HandleCrouch(ref crouch);
        MoveHorizontally(move);
        if (jump && grounded && !crouch) Jump();
    }

    private void HandleCrouch(ref bool crouch)
    {
        if (!crouch && Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround))
            crouch = true;

        if (crouch != wasCrouching)
        {
            wasCrouching = crouch;
            OnCrouchEvent.Invoke(crouch);
            if (crouchDisableCollider) crouchDisableCollider.enabled = !crouch;
        }
    }

    private void MoveHorizontally(float move)
    {
        float finalMove = wasCrouching ? move * crouchSpeedMultiplier : move;
        Vector3 targetVelocity = new(finalMove * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        if ((move > 0 && !facingRight) || (move < 0 && facingRight))
            Flip();
    }

    private void Jump()
    {
        grounded = false;
        rb.AddForce(Vector2.up * jumpForce * jumpMultiplier);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsGrounded() => grounded;
    public bool IsCeilingReached() => Physics2D.OverlapCircle(ceilingCheck.position, CeilingRadius, whatIsGround);
    public float GetVerticalVelocity() => rb.velocity.y;
    public float GetHorizontalVelocity() => rb.velocity.x;
}
