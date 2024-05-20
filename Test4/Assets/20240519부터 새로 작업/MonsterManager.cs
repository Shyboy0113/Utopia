using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int monsterCode; // ���� �ڵ� (���� 4�ڸ�)
    public GameObject monsterPrefab; // ���� ������

    private MonsterMovement monsterMovement;
    private MonsterStatus monsterStatus;
    private MonsterAnimation monsterAnimation;

    void Start()
    {
        // Resources �������� ���� ������ �ҷ�����
        monsterPrefab = Resources.Load<GameObject>($"Monsters/{monsterCode}");

        if (monsterPrefab != null)
        {
            GameObject monster = Instantiate(monsterPrefab, transform.position, transform.rotation);
            monsterMovement = monster.GetComponent<MonsterMovement>();
            monsterStatus = monster.GetComponent<MonsterStatus>();
            monsterAnimation = monster.GetComponent<MonsterAnimation>();
        }
        else
        {
            Debug.LogError($"Monster with code {monsterCode} not found in Resources.");
        }
    }
}
