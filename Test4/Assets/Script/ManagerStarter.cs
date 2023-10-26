using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStarter : MonoBehaviour
{
    
    public GameObject player;
    public GameObject mainCamera;

    void Awake()
    {
        GameManager.Instance.isPause = false;
    }

    private void Start()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(mainCamera);
        
    }

    public void Pause()
    {
        GameManager.Instance.isStory = true;
    }
    public void Release()
    {
        GameManager.Instance.isStory = false;
    }
    
}
