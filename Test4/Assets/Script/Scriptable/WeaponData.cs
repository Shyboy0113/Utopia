using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Object/Weapon Data")]
public class WeaponData : ScriptableObject
{

    public string weaponName;

    public int attackPower;
    public int durability;
    public int current_Durability;
    
    public string attribute;

    public void ReduceDurability(int amount)
    {
        current_Durability -= amount;
        if (current_Durability < 0)
        {
            current_Durability = 0;
        }
    }

    // Method to check if the weapon has durability remaining
    public bool HasDurability()
    {
        return current_Durability > 0;
    }
}
