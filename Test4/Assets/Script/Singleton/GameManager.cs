using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    // 적 스폰 코드
    public string enemyCode;

    // 배틀이 끝나고 스토리로 넘어가게
    public string storyCode;

    public void SetEnemyInfo(string code)
    {
        enemyCode = code;
    }

    // 체력 관련
    public int _playerHp;
    public int MaxHp = 5;
    
    //스태미나 관련
    public float playerStamina;
    public float maxStamina = 2.0f;
    public int staminaVector = 1;

    // 공격력 관련
    public int Atk;

    // 게임 플레이 도중 참고할 데이터들
    public int HpPotion = 0;
    public int Gold = 0;

    public void GetGold(int gold)
    {
        Gold += gold;
    }

    // 포션 중독 관련 게이지
    public float PotionAddiction = 0f;

    // 전투가 끝났을 때 (플레이어가 죽거나, 적이 죽음)
    public bool isBattle = false;

    public bool isExhausted = false;

    // 스토리 및 컷신 연출 중일 때
    public bool isStory = false;
    // 일시 정지
    public bool isPause = false;


    private void Start()
    {
        _playerHp = MaxHp;
        playerStamina = maxStamina;
        staminaVector = 1;
}

    private void Update()
    {
        //스태미나 관련
        if (!isExhausted)
        {
            if (playerStamina >= maxStamina) playerStamina = maxStamina;
            playerStamina += staminaVector * Time.deltaTime;
        }

        if (!isStory)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause) PauseGame();
                else ResumeGame();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                // RestartGame();
            }
        }

        // 포션 중독 관련 게이지
        if (PotionAddiction >= 5.0f)
        {
            Debug.Log("포션에 중독되었다.");
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

    // 치명적인 버그 나왔을 때 진행
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
