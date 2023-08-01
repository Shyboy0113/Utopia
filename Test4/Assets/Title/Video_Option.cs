using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Video_Option : MonoBehaviour
{
    public GameObject videoOptionsPanel;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown windowModeDropdown; // New Dropdown for window mode selection

    private Resolution[] resolutions;
    private Resolution selectedResolution;
    
    private void Start()
    {
        videoOptionsPanel.SetActive(false); // Hide the video options panel on start
        InitializeResolution();
        InitializeResolutionDropdown();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CloseVideoOptions();
    }

    public void InitializeResolution() // https://giseung.tistory.com/19
    {
        int setWidth = 1920; // 사용자 설정 너비
        int setHeight = 1080; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), Screen.fullScreenMode); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
    
    private void InitializeResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            int roundedRefreshRate = Mathf.RoundToInt(resolutions[i].refreshRate);
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + roundedRefreshRate + "Hz";

            if (!options.Contains(option))
            {
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetFullScreen(int screenIndex)
    {
        switch (screenIndex)
        {
            case 0: //창모드
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 1: //테두리 없음
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2: // 전체화면
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;

        }

        windowModeDropdown.value = screenIndex;
        windowModeDropdown.RefreshShownValue();
    }
    public void Set_NewResolution(int resolutionIndex)
    {
        selectedResolution = resolutions[resolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreenMode);
    }
    public void OpenVideoOptions()
    {
        videoOptionsPanel.SetActive(true);
    }

    public void CloseVideoOptions()
    {
        videoOptionsPanel.SetActive(false);
    }

}
