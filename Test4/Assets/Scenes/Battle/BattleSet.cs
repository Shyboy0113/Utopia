using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSet : MonoBehaviour
{
    private string _enemyName;

    public GameObject pistolPrefab; // 피스톨 프리팹
    public GameObject player;
    
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public float pistolSpeed = 20f; // 피스톨 이동 속도

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;

    void Awake()
    {
        // player의 RectTransform을 가져옴
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();
    }

    void Start()
    {
        _enemyName = GameManager.Instance.enemyName;

        if (_enemyName is not null)
        {
            spawnEnemy();
        }

    }

    private void Update()
    {
        MovePlayer();
        
        // Z키 입력 감지
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("빵");
            FirePistol();
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 입력 방향을 정규화하여 방향 벡터 생성
        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        // 정규화된 방향으로 일정한 속도를 적용
        playerRigidbody.velocity = direction * moveSpeed;
    }

    void FirePistol()
    {
        // 플레이어 위치에서 피스톨 생성
        GameObject newPistol = Instantiate(pistolPrefab, playerTransform.position, Quaternion.identity, transform);
        StartCoroutine(MovePistol(newPistol.transform));
    }

    IEnumerator MovePistol(Transform pistolTransform)
    {
        while (pistolTransform.position.y < Screen.height)
        {
            // 위로 이동
            pistolTransform.position += Vector3.up * pistolSpeed * Time.deltaTime;
            yield return null;
        }

        // 화면 밖으로 나가면 소멸
        Destroy(pistolTransform.gameObject);
    }

    public void spawnEnemy()
    {
        // 적 생성 로직
    }
}
