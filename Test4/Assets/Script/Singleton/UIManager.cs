using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    
    public Canvas PauseMenu;

    public void ResetAllMenu()
    {
        PauseMenu.gameObject.SetActive(false);
    }

    public void ActivatePauseMenu(bool pauseState)
    {
        PauseMenu.gameObject.SetActive(pauseState);
    }
    
}
