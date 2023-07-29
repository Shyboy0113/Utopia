using System.Collections;
using System.Collections.Generic;
using System.Numerics;

//안 넣으면 is an ambiguous reference가 나옴
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerShooting : MonoBehaviour
{
    private int directionX = 1;
    
    public GameObject bulletPrefab; // Reference to the bullet prefab

    private InventoryManager inventoryManager;
    private Weapon currentWeapon; // Store the currently equipped weapon
    private WeaponData currentWeaponData;
    
    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        currentWeaponData = currentWeapon.GetComponent<WeaponData>();
    }

    private void Update()
    {
        // Get the currently equipped weapon from the inventory manager
        currentWeapon = inventoryManager.GetCurrentWeapon();

        Debug.Log(currentWeapon);


        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Check A");
            
            if (currentWeapon is not null && currentWeapon.HasDurability())
            {

                Debug.Log("Check B");
                directionX = Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1;
                
                // Shoot and reduce durability when the player presses the attack button
                FireBullet();
                
                // Reduce the weapon's durability (you can adjust the amount as needed)
                currentWeapon.ReduceDurability(1);

                // If the weapon has no durability left, you can handle it as needed (e.g., remove it from the inventory)
                if (!currentWeapon.HasDurability())
                {
                    // Remove the weapon from the inventory or handle it based on your game logic
                }
            }
            else
            {
                // Handle the case when the player has no weapon equipped or when the weapon has no durability left
                // For example, display a message or switch to a default weapon, etc.
                Debug.Log("공격할 수 없습니다!");
            }
        }
    }

    private void FireBullet()
    {
        Debug.Log("Firing Bullet");
        
        //특정 Bullet 인스턴스의 속성과 상호 작용하거나 속성을 수정하려면 해당 스크립트 구성 요소에 대한 참조가 필요
        GameObject _bullet = Instantiate(bulletPrefab,  transform.position, transform.rotation);
        Rigidbody2D rigid_bullet = _bullet.GetComponent<Rigidbody2D>();
        rigid_bullet.AddForce(Vector3.right*10,ForceMode2D.Impulse);
        
        Bullet bulletScript = _bullet.GetComponent<Bullet>();
        bulletScript.SetBulletProperties(currentWeaponData.attackPower, currentWeaponData.attribute);
        
        
        Destroy(_bullet,2f);
        
    }
}