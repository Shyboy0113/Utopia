using System;
using UnityEngine;
public class RedRat : Monster
{
    // TEST
    [SerializeField] public Vector3 position = new Vector3(10, 0, 0);
    [SerializeField] public int hp = 5;
    private Vector3 _playerPosition = new Vector3(0, 0, 0);
    
    
    private enum RedRatStateEnum
    {
        IDLE,
        CHASE,
        ATTACK,
        DEATH
    }

    private RedRatStateEnum _currentState;
    private FSM _fsm;
    
    
    //Speed
    public float chargeSpeed = 2.0f;
    private Transform playerTransform;
    private Rigidbody2D _rigidbody;

    //Groggy
    public float groggyThreshold = 5.5f;
    private float currentGroggyValue = 3.0f;
    private bool isChasing = true;
    
    //Animation
    private Animator animator;
    private bool m_FacingRight = true;
    
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        transform.position = position;
        _currentState = RedRatStateEnum.IDLE;
        _fsm = new FSM(new RedRat_IdleState(this));
    }

    private void Update()
    {
        Flip();
        
        switch (_currentState)
        {
            case RedRatStateEnum.IDLE:
                if (IsHpZero(this) == false)
                {
                    if (CanAttackPlayer(this))
                    {
                        ChangeState(RedRatStateEnum.ATTACK);
                    }
                    else if (CanSeePlayer(this))
                    {
                        ChangeState(RedRatStateEnum.CHASE);
                    }
                    // TEST
                    else
                    {
                        _playerPosition = new Vector3(_playerPosition.x + 1, 0, 0);
                    }
                }
                else
                {
                    ChangeState(RedRatStateEnum.DEATH);
                }
                break;
            case RedRatStateEnum.CHASE:
                if (IsHpZero(this) == false)
                {
                    if (CanAttackPlayer(this))
                    {
                        ChangeState(RedRatStateEnum.ATTACK);
                    }
                    else if (CanSeePlayer(this) == false)
                    {
                        ChangeState(RedRatStateEnum.IDLE);
                    }
                }
                else
                {
                    ChangeState(RedRatStateEnum.DEATH);
                }
                break;
            case RedRatStateEnum.ATTACK:
                if (IsHpZero(this) == false)
                {
                    if (CanAttackPlayer(this) == false)
                    {
                        if (CanSeePlayer(this))
                        {
                            ChangeState(RedRatStateEnum.CHASE);
                        }
                        else
                        {
                            ChangeState(RedRatStateEnum.IDLE);
                        }
                    }
                }
                else
                {
                    ChangeState(RedRatStateEnum.DEATH);
                }
                break;
            case RedRatStateEnum.DEATH:
                // TODO: DEATH 코드 구현
                break;
            default:
                break;
        }
        _fsm.UpdateState();
    }
    
    
    private void ChangeState(RedRatStateEnum nextState)
    {
        _currentState = nextState;
        switch (_currentState)
        {
            case RedRatStateEnum.IDLE:
                _fsm.ChangeState(new IdleState(this));
                break;
            case RedRatStateEnum.CHASE:
                _fsm.ChangeState(new ChaseState(this));
                break;
            case RedRatStateEnum.ATTACK:
                _fsm.ChangeState(new AttackState(this));
                break;
            case RedRatStateEnum.DEATH:
                _fsm.ChangeState(new DeathState(this));
                break;
            default:
                break;
        }
    }

    void Flip()
    {
        int directionToPlayer = (playerTransform.position.x > transform.position.x ? 1 : -1);

        if (isChasing && playerTransform != null)
            if((directionToPlayer > 0 && !m_FacingRight) || (directionToPlayer < 0 && m_FacingRight))
        {
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    
    private bool CanSeePlayer(RedRat _redRat)
    {
        Debug.Log("[CanSeePlayer] Distance: " + Vector3.Distance(_redRat.transform.position, _playerPosition));
        if (Vector3.Distance(_redRat.transform.position, _playerPosition) < 10f)
        {
            return true;
        }

        return false;
    } 
    
    private bool CanAttackPlayer(RedRat _redRat)
    {
        Debug.Log("[CanAttackPlayer] Distance: " + Vector3.Distance(_redRat.transform.position, _playerPosition));
        if (Vector3.Distance(_redRat.transform.position, _playerPosition) < 5f)
        {
            return true;
        }

        return false;
    }

    private bool IsHpZero(RedRat _redRat)
    {
        Debug.Log("[IsHpZero] hp: " + _redRat.hp);
        if (_redRat.hp <= 0)
        {
            return true;
        }

        return false;
    }
    
    
    
    
    
}
