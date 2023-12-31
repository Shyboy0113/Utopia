using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSet : MonoBehaviour
{
    private string _enemyName;

    public GameObject pistolPrefab; // �ǽ��� ������
    public GameObject player;
    
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    public float pistolSpeed = 20f; // �ǽ��� �̵� �ӵ�

    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;

    void Awake()
    {
        // player�� RectTransform�� ������
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
