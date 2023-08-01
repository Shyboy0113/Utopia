using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Button : MonoBehaviour
{
    public void NewGame()
    {
        string targetSceneName = "Test"; // Replace "TargetScene" with the name of your target scene
        LoadingSceneController.LoadScene(targetSceneName);
    }

    public void LoadGame()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
