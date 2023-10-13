using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using PixelCrushers.DialogueSystem.UnityGUI.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public static int _playerHp;
    public static float _playerStamina;

    public static float _staminaMax = 2.0f;
    private int _staminaVector = 1;

    //Guard(경계 자세) 이벤트
    public UnityEvent OnGuardPostureActivated;
    public UnityEvent OnGuardPostureDeactivated;

    //스토리 및 컷신 연출 중일 때
    public bool isStory = false;
    
    //일시 정지
    public bool isPause = false;

    private void Start()
    {
        _playerHp = 5;
        _playerStamina = _staminaMax;
        
    }

    private void Update()
    {
        if (!isStory)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if( isPause is false)PauseGame();
                else ResumeGame();

            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) _staminaVector *= -1;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) _staminaVector *= -1;

        _playerStamina += _staminaVector * Time.deltaTime;
        if (_playerStamina >= _staminaMax) _playerStamina = _staminaMax;
        if (_playerStamina <= 0) _playerStamina = 0;

        Debug.Log(_playerStamina);

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

    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
    
}
