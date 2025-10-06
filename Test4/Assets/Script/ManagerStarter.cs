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
        GameManager.Instance.IsPaused = false;
    }

    private void Start()
    {
        //DontDestroyOnLoad(player);
        //DontDestroyOnLoad(mainCamera);
        
    }

    public void Pause()
    {
        GameManager.Instance.IsStory = true;    }
    public void Release()
    {
        GameManager.Instance.IsStory = false;
    }

    private void OnDestroy()
    {
        //Destroy(player);
        //Destroy(mainCamera);
    }
}
