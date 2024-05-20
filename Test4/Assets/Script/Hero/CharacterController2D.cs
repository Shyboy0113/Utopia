using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // ĳ���� �̹���
    public Animator animator;
    private bool _facingRight = true;

    // ĳ���� ���� ó��
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private float _jumpForce = 500f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;

    // ���� ó��
    [SerializeField] private bool _airControl = true; // ���� �߿��� ���� �����ϵ��� ����
    [SerializeField] private LayerMask _whatIsGround; // ĳ���Ͱ� �ν��ϴ� ������ ���̾�
    [SerializeField] private Transform _groundCheck; // ���� Ȯ�� ��ġ

    const float _groundedRadius = .2f; // ���� Ȯ���� ���� ������ ���� ������
    public bool _grounded;            // ĳ���Ͱ� ���鿡 �ִ��� ����

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

        // ���� Ȯ���� ���� _groundCheck ��ġ�� ���� �׷��� ����� �浹 ���θ� �Ǵ�
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
        // ���鿡 �ְų� ���� ������ ������ �� �̵� ó��
        if (_grounded || _airControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, _rigidBody2D.velocity.y);
            // �̵� �ӵ��� �ε巴�� ó���Ͽ� ĳ���Ϳ� ����
            _rigidBody2D.velocity = Vector3.SmoothDamp(_rigidBody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

            // ��������Ʈ �̹��� ��ȯ
            if (move > 0 && !_facingRight)
                Flip();
            else if (move < 0 && _facingRight)
                Flip();

        }
        // ���� ó��
        if (_grounded && jump)
        {
            // ĳ���Ϳ� ���� ���� �߰�
            _grounded = false;
            _rigidBody2D.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    private void Flip()
    {
        // ĳ���Ͱ� �ٶ󺸴� ���� ��ȯ
        _facingRight = !_facingRight;

        // ĳ������ x �� ���� �������� -1�� ���Ͽ� ���� ��ȯ
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
