using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Shadow : MonoBehaviour
{
    public Transform character; // Reference to the character's transform

    private void FixedUpdate()
    {
        // Calculate the position for the boss
        Vector3 oppositePosition = CalculateOppositePosition();

        // Set the boss's position
        transform.position = oppositePosition;
        Debug.Log("그림자 이동");
    }

    private Vector3 CalculateOppositePosition()
    {
        // Get the character's position
        Vector3 characterPosition = character.position;

        // Calculate the opposite position
        Vector3 oppositePosition = Vector3.zero - characterPosition;

        return oppositePosition;
    }
}
