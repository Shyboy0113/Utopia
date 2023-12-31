using System.Collections;
using UnityEngine;

public class BattleSet_UI : MonoBehaviour
{
    private string _enemyName;

    public GameObject pistolPrefab; // �ǽ��� ������
    public GameObject player;
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    public float pistolSpeed = 10f; // �ǽ��� �̵� �ӵ�

    private RectTransform playerRectTransform;

    void Start()
    {
        _enemyName = GameManager.Instance.enemyName;

        if (_enemyName is not null)
        {
            spawnEnemy();
        }

        // player�� RectTransform�� ������
        playerRectTransform = player.GetComponent<RectTransform>();
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
        Debug.Log("�����̴���");
        // Ű �Է¿� ���� �̵�
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;

        // ���� ��ġ ������Ʈ
        Vector2 newPosition = playerRectTransform.anchoredPosition + new Vector2(horizontal, vertical) * Time.deltaTime;

        // ���� ����
        newPosition.x = Mathf.Clamp(newPosition.x, -480, 480);
        newPosition.y = Mathf.Clamp(newPosition.y, -270, 270);

        // ��ġ ����
        playerRectTransform.anchoredPosition = newPosition;
    }

    void FirePistol()
    {
        // �÷��̾� ��ġ���� �ǽ��� ����
        GameObject newPistol = Instantiate(pistolPrefab, playerRectTransform.position, Quaternion.identity, transform);
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
