using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int score;

    private void Start()
    {
        // 게임 매니저의 인스턴스에 접근하여 변수를 사용할 수 있습니다.
        score = 0;
    }

    public void ResumeGame()
    {
        
    }
    
    
    public void RestartGame()
    {
        
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
    
}
