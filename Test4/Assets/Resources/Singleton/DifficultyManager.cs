using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager>
{
    public enum Difficulty
    {
        EASY = 48,
        NORMAL = 36,
        HARD = 24
    }
    
    public int standardOfDay;
    
    private Difficulty _currentDifficulty = Difficulty.NORMAL;
    public Difficulty CurrentDifficulty
    {
        get => _currentDifficulty;
        set
        {
            _currentDifficulty = value;
            
            //enum을 int로 명시적 형변환
            standardOfDay = (int)_currentDifficulty;
        }
    }


    public void SetDifficulty(int index)
    {
        switch (index)
        {
            case 0:
                CurrentDifficulty = Difficulty.EASY;
                break;
            case 1:
                CurrentDifficulty = Difficulty.NORMAL;
                break;
            case 2:
                CurrentDifficulty = Difficulty.HARD;
                break;
            default:
                CurrentDifficulty = Difficulty.NORMAL;
                break;
        }
    }
    
    
}
