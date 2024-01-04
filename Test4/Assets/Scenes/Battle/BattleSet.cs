using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSet : MonoBehaviour
{
    //적 소환 코드
    private string _enemyCode;

    //적 소환 위치
    public Transform spawnPoint;
    
    public GameObject pistolPrefab; // 피스톨 프리팹
    public GameObject player;
    
    public float moveSpeed = 5f; // 플레이어 이동 속도

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;

    //배틀 시작시 Hp차는 이펙트 구현
    public Image hpBar;
    public AnimationCurve easeOutCurve;
    private bool isBattle = false;

    public float fillTime = 3f;

    public GameObject enemy;
    EnemyState enemyState;

    //적 체력
    public int enemyCurrentHp;
    public int enemyMaxHp;

    //플레이어 체력
    public int playerCurrentHp;
    public int playerMaxHp;

    //플레이어 체력 UI
    public TMP_Text playerName;
    public TMP_Text playerHp;
    public Image playerHpBar;

    //텍스트 모음
    public GameObject textBox; // 조작 키 설명




    void Awake()
    {
        // player의 RectTransform을 가져옴
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();

        GetEnemyInfo();

        GameManager.Instance.isBattle = false;

        if (_enemyCode is not null)
        {
            spawnEnemy();
        }

        enemy = GameObject.FindWithTag("Enemy");

    }

    void Start()
    {

        if (enemy is not null)
        {
            enemyState = enemy.GetComponent<EnemyState>();

            enemyMaxHp = enemyState.currentHp;
            enemyCurrentHp = enemyMaxHp;
        }

        playerMaxHp = GameManager.Instance.MaxHp;
        playerCurrentHp = playerMaxHp;

        hpBar.fillAmount = 0f;

        StopAllCoroutines();
        StartCoroutine(FillImageWithCurve(hpBar, fillTime));


    }

    IEnumerator FillImageWithCurve(Image image, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            float curveProgress = elapsedTime / time;
            float curveValue = easeOutCurve.Evaluate(curveProgress);
            image.fillAmount = curveValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.fillAmount = 1.0f;
        GameManager.Instance.isBattle = true;

        yield return new WaitForSeconds(5f);

        textBox.SetActive(false);

    }

    private void Update()
    {
        isBattle = GameManager.Instance.isBattle;

        //플레이어 Hp바 텍스트 조정
        playerHp.text = $"HP : {playerCurrentHp}/{playerMaxHp}";
        playerHpBar.fillAmount = (float)playerCurrentHp / playerMaxHp;

        if (isBattle is true)
        {
            MovePlayer();        

            // Z키 입력 감지
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("빵");
                FirePistol();
            }

            enemyCurrentHp = enemyState.currentHp;

            hpBar.fillAmount = (float)enemyCurrentHp / enemyMaxHp;
            
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
        
    }

    public void GetEnemyInfo()
    {
        _enemyCode = GameManager.Instance.enemyCode;
    }
    public void spawnEnemy()
    {
        // 해당 몬스터 프리팹 로드
        GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Monsters/" + _enemyCode);

        if (enemyPrefab != null)
        {
            // 몬스터 인스턴스화
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

}
