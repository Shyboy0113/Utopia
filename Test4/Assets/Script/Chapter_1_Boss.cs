using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter_1_Boss : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public float chargeSpeed = 5.0f;
    public float groggyThreshold = 5.0f;
    private float currentGroggyValue = 0.0f;

    private Transform playerTransform;
    private bool isChasing = false;

    private void Start()
    {
        currentHealth = maxHealth;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isChasing && playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.Translate(directionToPlayer * chargeSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isChasing)
        {
            currentGroggyValue += 1.0f;

            if (currentGroggyValue >= groggyThreshold)
            {
                // 보스가 그로기 상태에 돌입할 때의 동작 처리
                // 예: 애니메이션 변경, 공격 불가 상태 등
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
}
