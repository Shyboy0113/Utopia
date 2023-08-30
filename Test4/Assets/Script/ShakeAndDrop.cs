using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAndDrop : MonoBehaviour
{
    public float shakeDuration = 3.0f; // 흔들리는 시간
    public float shakeIntensity = 0.01f; // 흔들림 강도
    public float fallSpeed = 10.0f; // 떨어지는 속도

    private bool isShaking = false;
    private Vector3 originalPosition;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        originalPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌했습니다."+ collision);
     
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy")) Destroy(gameObject); 
        
            if (collision.contacts[0].normal.y == -1.0f && !isShaking && _rigidbody.bodyType == RigidbodyType2D.Kinematic)
        {
            isShaking = true;
            Invoke(nameof(StartFalling), shakeDuration);
        }

    }

    private void Update()
    {
        if (isShaking) ShakeBlock();
    }

    private void ShakeBlock()
    {
        Vector3 shakeOffset = Random.insideUnitCircle * shakeIntensity;
        transform.position = originalPosition + shakeOffset;
    }

    private void StartFalling()
    {
        isShaking = false;
        transform.position = originalPosition;
        
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

}
