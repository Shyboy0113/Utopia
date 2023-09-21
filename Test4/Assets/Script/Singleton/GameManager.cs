using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using PixelCrushers.DialogueSystem.UnityGUI.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    //스토리 및 컷신 연출 중일 때
    public bool isStory = false;
    
    //일시 정지
    public bool isPause = false;
    
    private void Update()
    {
        if (!isStory)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();

            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPause = !isPause;

        UIManager.Instance.ActivatePauseMenu(isPause);
    }

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1;
        
        UIManager.Instance.ActivatePauseMenu(isPause);
    }
    
    //치명적인 버그 나왔을 때 진행
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        UIManager.Instance.ResetAllMenu();
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
        UIManager.Instance.ResetAllMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
    
}
