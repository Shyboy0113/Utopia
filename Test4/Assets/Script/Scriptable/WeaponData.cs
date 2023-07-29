using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Object/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int attackPower;
    public int durability;
    public string attribute;

}
