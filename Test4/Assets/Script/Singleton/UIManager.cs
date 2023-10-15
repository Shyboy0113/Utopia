using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject PauseMenu;
    
    public float animationDuration = 0.1f;
    public float desiredNarrowedHeight = 100.0f;
    
    public void ResetAllMenu()
    {
        if (PauseMenu is not null) PauseMenu.gameObject.SetActive(false);
    }

    public void ActivatePauseMenu(bool pauseState)
    {
        if (PauseMenu is not null) PauseMenu.gameObject.SetActive(pauseState);
    }
    
}
