using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Boss : MonoBehaviour
{
    public Transform character; // Reference to the character's transform

    void Start()
    {  
        // Repeat repositioning of the boss.
        InvokeRepeating("Teleport",5,5);
    }

    private void Teleport()
    {
        // Set the boss's position to Hero's position
        transform.position = character.position + new Vector3(0,3,0);
        Debug.Log("보스 이동");
    }
}
