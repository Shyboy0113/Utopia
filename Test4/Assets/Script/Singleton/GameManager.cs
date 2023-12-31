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

    public string enemyName;
    
    //체력 관련
    public int _playerHp;
    public int MaxHp = 3;
    
    //스태미나 관련
    public float _playerStamina;
    public float _staminaMax = 5.0f;
    private float _staminaVector = 1f;

    //게임 플레이 도중 참고할 데이터들
    public int HpPotion = 0;
    public int Gold = 0;

    //포션 중독 관련 게이지
    public float PotionAddiction = 0f;
    
    //Guard(경계 자세) 이벤트
    public UnityEvent OnGuardPostureActivated;
    public UnityEvent OnGuardPostureDeactivated;

    //스토리 및 컷신 연출 중일 때
    public bool isStory = false;
    //일시 정지
    public bool isPause = false;

    private void Start()
    {
        _playerHp = MaxHp;
        _playerStamina = _staminaMax;

    }

    private void Update()
    {
        if (!isStory)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause is false) PauseGame();
                else ResumeGame();

            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                //RestartGame();
            }

            if (!isPause){

                if (Input.GetKeyDown(KeyCode.LeftShift) && !MoveCharacter.isGroggy)
                {
                    _staminaVector *= -5f;
                    Time.timeScale = 0.2f;
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift) && !MoveCharacter.isGroggy)
                {
                    _staminaVector = 1.0f;
                    Time.timeScale = 1.0f;
                }
            }

        }

        //스태미나 채움 관련 이벤트
        _playerStamina += _staminaVector * Time.deltaTime;
        if (_playerStamina >= _staminaMax) _playerStamina = _staminaMax;
        
        if (_playerStamina <= 0 && !MoveCharacter.isGroggy) {
            
            StartCoroutine(GroggyCoroutine(3f));
            var player = GameObject.FindGameObjectWithTag("Player");
            var moveCharacter = player.GetComponent<MoveCharacter>();
            if (moveCharacter is not null)
            {
                moveCharacter.StartCoroutine(moveCharacter.StartGroggy(3f));
            }

            _playerStamina = 0.00000000001f;

        }
        
        //포션 중독 관련 게이지
        if (PotionAddiction >= 5.0f)
        {
            Debug.Log("포션에 중독되었다.");
        } 

    }
    
    private IEnumerator GroggyCoroutine(float duration)
    {
        _staminaVector = 0.0f;
        Time.timeScale = 1.0f;

        yield return new WaitForSeconds(duration);

        // After the specified duration, set isGroggy to false and play idle animation
        MoveCharacter.isGroggy = false;
        _staminaVector = 1.0f;
        
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
