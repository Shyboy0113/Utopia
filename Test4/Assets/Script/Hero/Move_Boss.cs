using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Boss : MonoBehaviour
{
    public Transform character; // Reference to the character's transform
    private void FixedUpdate()
    {
        // Repeat repositioning of the boss.
        InvokeRepeating("Teleport",2,2);
        Debug.Log("이동 완료");
        
    }

    private void Teleport()
    {
        // Set the boss's position to Hero's position
        transform.position = character.position;
    }
}
