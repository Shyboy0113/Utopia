using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D characterRigidbody;
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // -1 ~ 1

        Vector3 velocity = new Vector3(inputX, inputY, 0);
        velocity *= speed;
        characterRigidbody.velocity = velocity;

    }

}
