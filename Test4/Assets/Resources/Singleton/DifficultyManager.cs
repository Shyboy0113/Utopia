using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager>
{
    public enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }
    
    private Difficulty _currentDifficulty = Difficulty.NORMAL;
    public Difficulty CurrentDifficulty
    {
        get => _currentDifficulty;
        set
        {
            _currentDifficulty = value;
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
