using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Monster_Attribute
{
    Normal,
    Red,
    Blue,
    Yellow,
    
}

[CreateAssetMenu(fileName = "New Monster", menuName = "Scriptable Object/Monster Data")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    
    public int attackPower;
    public int maxHp;
    public int currentHp;
    
    public Monster_Attribute attribute;

    public void ReduceDurability(int amount)
    {
        currentHp -= amount;
        if (currentHp < 0)
        {
            currentHp = 0;
        }
    }
    
    public bool HasHp()
    {
        return currentHp > 0;
    }
}
