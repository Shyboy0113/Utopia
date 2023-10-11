using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageSize : MonoBehaviour
{
    public GameObject player;
    
    private float playerXPos;
    private float PortalXPos = 65;

    private Rigidbody2D _rigidbody2D;

    public Image image;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player is null) GameObject.Find("Player");
        
    }

    private void Start()
    {
        _rigidbody2D = player.GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        playerXPos = player.transform.position.x;
        image.rectTransform.position += new Vector3(0,_rigidbody2D.velocity.x , 0).normalized * -0.01f;
        image.rectTransform.localScale = new Vector3(1+playerXPos/(PortalXPos * 10f), 1+playerXPos/(PortalXPos * 5f), 1);
    }
}
