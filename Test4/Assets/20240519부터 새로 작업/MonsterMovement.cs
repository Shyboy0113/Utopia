using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 간단한 AI 움직임 예시
        movement = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
