using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int monsterCode; // 몬스터 코드 (숫자 4자리)
    public GameObject monsterPrefab; // 몬스터 프리팹

    private MonsterMovement monsterMovement;
    private MonsterStatus monsterStatus;
    private MonsterAnimation monsterAnimation;

    void Start()
    {
        // Resources 폴더에서 몬스터 프리팹 불러오기
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
