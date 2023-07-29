using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public int current_Durability;
    
    public void InitializeWeaponDurability(){
        
        current_Durability = weaponData.durability;
        
    }
    
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
    
    public string GetWeaponName()
    {
        return weaponData.weaponName;
    }
}
