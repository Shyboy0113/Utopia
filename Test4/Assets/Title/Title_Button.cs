using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class Title_Button : MonoBehaviour
{
    public UnityEngine.UI.Toggle skipTutorialToggle;
    public GameObject skipCanvas;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void NewGame()
    {
        Debug.Log(PlayerPrefs.HasKey("SkipTutorial"));
        
        if (!PlayerPrefs.HasKey("SkipTutorial") || PlayerPrefs.GetInt("SkipTutorial") == 0)
        {
            skipCanvas.SetActive(true);   
        }
        else
        {
            PixelCrushers.SaveSystem.LoadScene("Tutorial");
        }

    }
    public void ChangeSkipPrefs()
    {
        // Save the toggle state to PlayerPrefs
        PlayerPrefs.SetInt("SkipTutorial", skipTutorialToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
        
    }
    public void GotoManipulationTutorial()
    {
        PixelCrushers.SaveSystem.LoadScene("Manipulation Tutorial");
    }
    
    public void SkipToTutorial()
    {
        PixelCrushers.SaveSystem.LoadScene("Tutorial");
    }

    public void LoadGame()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
