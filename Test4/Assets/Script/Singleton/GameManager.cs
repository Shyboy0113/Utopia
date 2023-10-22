using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem.UnityGUI.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public int _playerHp;
    public float _playerStamina;

    public float _staminaMax = 3.0f;
    private float _staminaVector = 1f;

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
                if( isPause is false )PauseGame();
                else ResumeGame();

            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _staminaVector *= -5f;
            Time.timeScale = 0.2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _staminaVector = 1.0f;
            Time.timeScale = 1.0f;
        }

        _playerStamina += _staminaVector * Time.deltaTime;
        if (_playerStamina >= _staminaMax) _playerStamina = _staminaMax;
        if (_playerStamina <= 0) {
            _playerStamina = 0;
        }

    }
    
    public void ChangeStoryState()
    {
        isStory = !isStory;
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
