using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    private float _speed = 20f;
    private float _lifeTime = 0.5f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, _lifeTime);
    }
    private void Update()
    {
        _rigidbody.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            EnemyState enemyState = other.GetComponent<EnemyState>();

            if (enemyState is not null)
            {
                enemyState.ChangeHp();
            }

            SoundEffectManager.Instance.PlayEffect(0);

            //Set Active off
            Destroy(gameObject);
        }
    }

}
