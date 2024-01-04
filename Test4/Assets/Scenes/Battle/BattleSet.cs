using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSet : MonoBehaviour
{
    //�� ��ȯ �ڵ�
    private string _enemyCode;

    //�� ��ȯ ��ġ
    public Transform spawnPoint;
    
    public GameObject pistolPrefab; // �ǽ��� ������
    public GameObject player;
    
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;

    //��Ʋ ���۽� Hp���� ����Ʈ ����
    public Image hpBar;
    public AnimationCurve easeOutCurve;
    private bool isBattle = false;

    public float fillTime = 3f;

    public GameObject enemy;
    EnemyState enemyState;

    //�� ü��
    public int enemyCurrentHp;
    public int enemyMaxHp;

    //�÷��̾� ü��
    public int playerCurrentHp;
    public int playerMaxHp;

    //�÷��̾� ü�� UI
    public TMP_Text playerName;
    public TMP_Text playerHp;
    public Image playerHpBar;

    //�ؽ�Ʈ ����
    public GameObject textBox; // ���� Ű ����




    void Awake()
    {
        // player�� RectTransform�� ������
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

        //�÷��̾� Hp�� �ؽ�Ʈ ����
        playerHp.text = $"HP : {playerCurrentHp}/{playerMaxHp}";
        playerHpBar.fillAmount = (float)playerCurrentHp / playerMaxHp;

        if (isBattle is true)
        {
            MovePlayer();        

            // ZŰ �Է� ����
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("��");
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

        // �Է� ������ ����ȭ�Ͽ� ���� ���� ����
        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;

        // ����ȭ�� �������� ������ �ӵ��� ����
        playerRigidbody.velocity = direction * moveSpeed;
    }

    void FirePistol()
    {
        // �÷��̾� ��ġ���� �ǽ��� ����
        GameObject newPistol = Instantiate(pistolPrefab, playerTransform.position, Quaternion.identity, transform);
        
    }

    public void GetEnemyInfo()
    {
        _enemyCode = GameManager.Instance.enemyCode;
    }
    public void spawnEnemy()
    {
        // �ش� ���� ������ �ε�
        GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Monsters/" + _enemyCode);

        if (enemyPrefab != null)
        {
            // ���� �ν��Ͻ�ȭ
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

}
