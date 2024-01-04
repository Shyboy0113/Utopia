using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private int _power;

    [SerializeField]
    private int _defence;

    [SerializeField]
    private int _maxHp;

    [SerializeField]
    private int _gold;

    [SerializeField]
    private int _exp;

    public string Name { get { return _name; } }   
    public int Power { get { return _power; } }
    public int Defence { get { return _defence; } }    
    public int MaxHp { get { return _maxHp; } }

    public int Gold { get { return _gold; } }
    public int Exp { get { return _exp; } }

}
