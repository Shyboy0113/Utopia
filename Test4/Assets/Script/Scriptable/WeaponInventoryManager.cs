using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryManager : Singleton<WeaponInventoryManager>
{
    public List<WeaponData> weaponInventory = new List<WeaponData>();
    private int currentWeaponIndex = 0;
    
    // Add methods for adding, removing, or checking weapons in the inventory
    // For simplicity, we'll just implement a basic method to add a weapon for this example:
    public void AddWeaponToInventory(WeaponData weapon)
    {
        weaponInventory.Add(weapon);
    }
    
    public void RemoveWeaponFromInventory(WeaponData weapon)
    {
        weaponInventory.Remove(weapon);
    }
    
    public WeaponData GetCurrentWeapon()
    {
        // Check if the currentWeaponIndex is within the bounds of the weaponInventory
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weaponInventory.Count)
        {
            return weaponInventory[currentWeaponIndex];
        }
        else
        {
            // Handle the case where the currentWeaponIndex is out of bounds (e.g., return a default weapon)
            return null;
        }
    }
    
}
