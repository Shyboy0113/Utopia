using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSet : MonoBehaviour
{
    private string _enemyName;

    public GameObject pistolPrefab; // �ǽ��� ������
    public GameObject player;
    
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    public float pistolSpeed = 20f; // �ǽ��� �̵� �ӵ�

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;

    public Image dummyHpBar;
    public Image hpBar;

    public float fillTime = 0.5f;

    public GameObject enemy;

    public int enemyCurrentHp;
    public int enemyMaxHp;


    void Awake()
    {
        // player�� RectTransform�� ������
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();

        enemy = GameObject.FindWithTag("Enemy");

        if (enemy is not null)
        {
            
        }

    }

    void Start()
    {
        hpBar.fillAmount = 0f;

        _enemyName = GameManager.Instance.enemyName;

        StopAllCoroutines();
        StartCoroutine(FillHp());

        if (_enemyName is not null)
        {
            spawnEnemy();
        }

    }
    IEnumerator FillHp()
    {
        float elapsedTime = 0;

        while (elapsedTime < fillTime)
        {
            dummyHpBar.fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / fillTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dummyHpBar.fillAmount = 1f;
        hpBar.gameObject.SetActive(true);
        dummyHpBar.gameObject.SetActive(false);

    }


    private void Update()
    {
        MovePlayer();
        
        // ZŰ �Է� ����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("��");
            FirePistol();
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
        StartCoroutine(MovePistol(newPistol.transform));
    }

    IEnumerator MovePistol(Transform pistolTransform)
    {
        while (pistolTransform.position.y < Screen.height)
        {
            // ���� �̵�
            pistolTransform.position += Vector3.up * pistolSpeed * Time.deltaTime;
            yield return null;
        }

        // ȭ�� ������ ������ �Ҹ�
        Destroy(pistolTransform.gameObject);
    }

    public void spawnEnemy()
    {
        // �� ���� ����
    }
}
