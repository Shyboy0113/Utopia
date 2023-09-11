using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossAI : MonoBehaviour
{
    //hp
    public int maxHealth = 3;
    private int currentHealth;

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
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        
    }

    private void Update()
    {
        int directionToPlayer = (playerTransform.position.x > transform.position.x ? 1 : -1);
        
        if (isChasing && playerTransform != null)
        {
            //Flip
            if (directionToPlayer > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (directionToPlayer < 0 && m_FacingRight)
            {
                Flip();
            }

            _rigidbody.velocity = new Vector2(directionToPlayer * chargeSpeed, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(currentGroggyValue);
        if (collision.gameObject.CompareTag("Obstacle") && isChasing)
        {
            currentGroggyValue += 0.5f;

            isChasing = false;
            animator.Play("Boss_Groggy");
            
            Invoke("Release_Groggy",currentGroggyValue);
            
            //타이머로 구현한 게 아니라 Invoke로 해서 그런듯
            
            
            if (currentGroggyValue >= groggyThreshold)
            {
                // 보스가 그로기 상태에 돌입할 때의 동작 처리
                // 예: 애니메이션 변경, 공격 불가 상태 등
            }
            
            
            Debug.Log(currentGroggyValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isChasing)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                // 보스 사망 처리
                // 예: 애니메이션 변경, 게임 종료 등
            }
            else if (currentHealth > 0)
            {
                // 주인공 공격 시 게임 오버 처리
                if (currentHealth <= 0)
                {
                    // 게임 오버 처리
                    // 예: 게임 오버 화면 표시, 게임 일시 정지 등
                }
            }
        }
    }

    void Rush()
    {
        isChasing = false;
        animator.Play("Boss_Rush");
        int directionToPlayer = (playerTransform.position.x > transform.position.x ? 1 : -1);
        // 보스가 주인공을 따라가던 것에서 x 값만 사용하여 좌우로 이동하도록 변경
        
        _rigidbody.velocity = new Vector2(directionToPlayer * chargeSpeed*10, 0);
        
    }
    
    void Release_Groggy()
    {
        Debug.Log("Idle 상태로 돌아옴");
        animator.Play("Boss_Idle");
        GameObject.Find("Boss").GetComponent<Activate_Gauge>().ResetFillGauge();
        isChasing = true;
    }
    
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

