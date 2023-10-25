using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStarter : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.isPause = false;
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
