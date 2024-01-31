using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    //캐릭터 이미지
    public Animator animator;
    private bool _facingRight = true;

    //캐릭터 물리 처리
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private float _jumpForce = 500f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;

    //지면 처리
    [SerializeField] private bool _airControl = false; // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask _whatIsGround; // A mask determining what is ground to the character
    [SerializeField] private Transform _groundCheck; // A position marking where to check if the player is _grounded.

    const float _groundedRadius = .2f; // Radius of the overlap circle to determine if _grounded
    public bool _grounded;            // Whether or not the player is _grounded.
    
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = _grounded;
        _grounded = false;

        // The player is _grounded if a circlecast to the _groundCheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public float GetVerticalVelocity()
    {
        return _rigidBody2D.velocity.y;
    }

    public float GetHorizontalVelocity()
    {
        return _rigidBody2D.velocity.x;
    }

    public void Move(float move, bool jump)
    {
        if (_grounded || _airControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, _rigidBody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            _rigidBody2D.velocity = Vector3.SmoothDamp(_rigidBody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

            // 스프라이트 이미지 전환
            if (move > 0 && !_facingRight)  Flip();            
            else if (move < 0 && _facingRight) Flip();
            
        }
        // If the player should jump...
        if (_grounded && jump)
        {
            // Add a vertical force to the player.
            _grounded = false;
            _rigidBody2D.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}