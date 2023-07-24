using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    public WeaponInventoryManager weaponInventoryManager;
    public WeaponData weaponToAdd;

    public void PrintWeaponData()
    {
        Debug.Log("무기 속성 :: " + weaponToAdd.attribute);
        Debug.Log("무기 공격력 :: " + weaponToAdd.attackPower);
        Debug.Log("무기 내구도 :: " + weaponToAdd.durability);
        Debug.Log("--------------------------------");
    }
}
