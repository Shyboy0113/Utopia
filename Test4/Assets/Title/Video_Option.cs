using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Video_Option : MonoBehaviour
{
    public GameObject videoOptionsPanel;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private Resolution selectedResolution;
    private bool isFullScreen;

    private void Start()
    {
        videoOptionsPanel.SetActive(false); // Hide the video options panel on start
        LoadPlayerPrefs();
        InitializeResolutionDropdown();
    }

    private void LoadPlayerPrefs()
    {
        isFullScreen = (PlayerPrefs.GetInt("FullScreen", 1) == 1);
        Screen.fullScreen = isFullScreen;
        fullscreenToggle.isOn = isFullScreen;
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

        selectedResolution = Screen.currentResolution;
    }

    public void SetResolution(int resolutionIndex)
    {
        selectedResolution = resolutions[resolutionIndex];
    }

    public void SetFullScreen(bool isFullScreen)
    {
        this.isFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
    }

    public void OpenVideoOptions()
    {
        videoOptionsPanel.SetActive(true);
    }

    public void CloseVideoOptions()
    {
        videoOptionsPanel.SetActive(false);
    }


    public void CancelResolutionChange()
    {
        CloseVideoOptions();
        // Optionally, you can reset the dropdown selection to match the current resolution if the user cancels.
    }

    public void ApplyResolutionChange()
    {
        if (SelectedResolutionDiffersFromCurrent())
        {
            ApplySelectedResolution();
        }

        CloseVideoOptions();
    }
    
    private bool SelectedResolutionDiffersFromCurrent()
    {
        return selectedResolution.width != Screen.currentResolution.width ||
               selectedResolution.height != Screen.currentResolution.height;
    }

    private void ApplySelectedResolution()
    {
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullScreen);
    }
}
