using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // 캐릭터 이미지
    public Animator animator;
    private bool _facingRight = true;

    // 캐릭터 물리 처리
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private float _jumpForce = 500f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;

    // 지면 처리
    [SerializeField] private bool _airControl = true; // 점프 중에도 조작 가능하도록 설정
    [SerializeField] private LayerMask _whatIsGround; // 캐릭터가 인식하는 지면의 레이어
    [SerializeField] private Transform _groundCheck; // 지면 확인 위치

    const float _groundedRadius = .2f; // 지면 확인을 위한 오버랩 원의 반지름
    public bool _grounded;            // 캐릭터가 지면에 있는지 여부

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

        // 지면 확인을 위해 _groundCheck 위치에 원을 그려서 지면과 충돌 여부를 판단
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
        // 지면에 있거나 공중 조작이 가능할 때 이동 처리
        if (_grounded || _airControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, _rigidBody2D.velocity.y);
            // 이동 속도를 부드럽게 처리하여 캐릭터에 적용
            _rigidBody2D.velocity = Vector3.SmoothDamp(_rigidBody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

            // 스프라이트 이미지 전환
            if (move > 0 && !_facingRight)
                Flip();
            else if (move < 0 && _facingRight)
                Flip();

        }
        // 점프 처리
        if (_grounded && jump)
        {
            // 캐릭터에 수직 힘을 추가
            _grounded = false;
            _rigidBody2D.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    private void Flip()
    {
        // 캐릭터가 바라보는 방향 전환
        _facingRight = !_facingRight;

        // 캐릭터의 x 축 로컬 스케일을 -1로 곱하여 방향 전환
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
