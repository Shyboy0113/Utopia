using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<Weapon> weaponInventory = new List<Weapon>();
    private int currentWeaponIndex = 0;
    
    // Add methods for adding, removing, or checking weapons in the inventory
    // For simplicity, we'll just implement a basic method to add a weapon for this example:
    public void AddWeaponToInventory(WeaponData weaponData)
    {
        Weapon newWeapon = new GameObject("NewWeapon").AddComponent<Weapon>();
        newWeapon.weaponData = weaponData;
        
        newWeapon.InitializeWeaponDurability();
        weaponInventory.Add(newWeapon);
    }
    
    public void RemoveWeaponFromInventory(Weapon weapon)
    {
        weaponInventory.Remove(weapon);
    }
    
    public Weapon GetCurrentWeapon()
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
