using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float detectionRadius = 5f; // 인식 범위
    public float sightAngle = 45f; // 시야각
    public LayerMask playerLayer;
    public GameObject questionMark;
    public GameObject exclamationMark;

    private Transform player;
    private bool isPlayerDetected = false;
    private bool isPlayerInSight = false;
    private bool isAlerted = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        questionMark.SetActive(false);
        exclamationMark.SetActive(false);
    }

    void Update()
    {
        DetectPlayer();
        if (isPlayerDetected)
        {
            CheckPlayerInSight();
        }
    }

    void DetectPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        if (hits.Length > 0)
        {
            isPlayerDetected = true;
            questionMark.SetActive(true);
        }
        else
        {
            isPlayerDetected = false;
            questionMark.SetActive(false);
            isAlerted = false;
            exclamationMark.SetActive(false);
        }
    }

    void CheckPlayerInSight()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector2.Angle(transform.right, directionToPlayer);

        if (angle < sightAngle / 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRadius, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                isPlayerInSight = true;
                Alert();
            }
        }
        else
        {
            isPlayerInSight = false;
        }
    }

    void Alert()
    {
        if (!isAlerted)
        {
            isAlerted = true;
            questionMark.SetActive(false);
            exclamationMark.SetActive(true);
            // 공격 모드로 전환
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        // 공격 로직 구현
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Vector3 forward = transform.right;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, sightAngle / 2) * forward;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -sightAngle / 2) * forward;

        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRadius);

        Gizmos.color = new Color(1, 1, 0, 0.2f); // 반투명 노란색
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}
