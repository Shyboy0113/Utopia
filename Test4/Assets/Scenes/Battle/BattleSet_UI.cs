using System.Collections;
using UnityEngine;

public class BattleSet_UI : MonoBehaviour
{
    private string _enemyName;

    public GameObject pistolPrefab; // 피스톨 프리팹
    public GameObject player;
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public float pistolSpeed = 10f; // 피스톨 이동 속도

    private RectTransform playerRectTransform;

    void Start()
    {
        _enemyName = GameManager.Instance.enemyName;

        if (_enemyName is not null)
        {
            spawnEnemy();
        }

        // player의 RectTransform을 가져옴
        playerRectTransform = player.GetComponent<RectTransform>();
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
        Debug.Log("움직이는중");
        // 키 입력에 따른 이동
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;

        // 현재 위치 업데이트
        Vector2 newPosition = playerRectTransform.anchoredPosition + new Vector2(horizontal, vertical) * Time.deltaTime;

        // 범위 제한
        newPosition.x = Mathf.Clamp(newPosition.x, -480, 480);
        newPosition.y = Mathf.Clamp(newPosition.y, -270, 270);

        // 위치 적용
        playerRectTransform.anchoredPosition = newPosition;
    }

    void FirePistol()
    {
        // 플레이어 위치에서 피스톨 생성
        GameObject newPistol = Instantiate(pistolPrefab, playerRectTransform.position, Quaternion.identity, transform);
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
