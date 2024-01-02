using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string enemyName;

    [SerializeField]
    private int power = 1;

    [SerializeField]
    private int defence = 0;

    [SerializeField]
    private int maxHp = 30;

    public string EnemyName { get { return enemyName; } }   
    public int Power { get { return power; } }
    public int Defence { get { return defence; } }    
    public int MaxHp { get { return maxHp; } }
    
}
