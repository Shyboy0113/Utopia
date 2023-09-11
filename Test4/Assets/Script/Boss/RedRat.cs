using System;
using RedRatStates;
using UnityEngine;
public class RedRat : Monster
{
    public enum RedRatStateEnum
    {
        IDLE,
        CHASE,
        CHARGE,
        RUSH,
        GROGGY,
        DEATH,
    }

    private RedRatStateEnum _currentState;
    private MonsterBaseState _state;
    
    
    //Speed
    public float chargeSpeed = 2.0f;
    private Transform _playerTransform;
    public Rigidbody2D _rigidbody;

    //Groggy
    public float currentGroggyValue = 3.0f;
    public int currentGroggyCount = 0;
    private bool isChasing = true;
    
    //Animation
    public Animator animator;
    public bool m_FacingRight = true;
    
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentState = RedRatStateEnum.CHASE;
        _state = new RRState_Chase(this);
    }

    private void Update()
    {
        _state.OnStateUpdate();
        Debug.Log(_state.ToString());
        
    }


    public void ChangeState(RedRatStateEnum nextState)
    {
        if (nextState == _currentState) return;
        
        _state.OnStateExit();
        
        _currentState = nextState;
        switch (_currentState)
        {
            case RedRatStateEnum.IDLE:
                _state = new RRState_Idle(this);
                break;
            case RedRatStateEnum.CHASE:
                _state = new RRState_Chase(this);
                break;
            case RedRatStateEnum.CHARGE:
                _state = new RRState_Charge(this);
                break;
            case RedRatStateEnum.RUSH:
                _state = new RRState_Rush(this);
                break;
            case RedRatStateEnum.GROGGY:
                _state = new RRState_Groggy(this);
                break;
            case RedRatStateEnum.DEATH:
                _state = new RRState_Death(this);
                break;
            default:
                break;
        }
        
        _state.OnStateEnter();
    }

    public void Flip()
    {
        int directionToPlayer = (_playerTransform.position.x > transform.position.x ? 1 : -1);
        if (isChasing && _playerTransform != null)
            if((directionToPlayer > 0 && !m_FacingRight) || (directionToPlayer < 0 && m_FacingRight))
        {
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    
    public bool CanSeePlayer(RedRat _redRat)
    {
        //Debug.Log("[CanSeePlayer] Distance: " + Vector3.Distance(_redRat.transform.position, _playerTransform.position));
        if (Vector3.Distance(_redRat.transform.position, _playerTransform.position) < 30f)
        {
            return true;
        }

        return false;
    } 
    
    public bool CanAttackPlayer(RedRat _redRat)
    {
        float dist = _redRat.transform.position.x - _playerTransform.position.x;
        dist = Mathf.Abs(dist);
        
        //Debug.Log("[CanAttackPlayer] Distance: " + dist);
        if (dist < 5f)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        //콜라이더의 이름이 Hidden_Trigger_Wall이면서, RedRat(이 스크립트를 갖고있는 개체)의 애니메이션 상태가 "Rush"라면,
        if (collider.gameObject.name == "Hidden_Trigger Wall" && _currentState == RedRatStateEnum.RUSH)
        {
            ChangeState(RedRatStateEnum.GROGGY);
        }
    }
}
