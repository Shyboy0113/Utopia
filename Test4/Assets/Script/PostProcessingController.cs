using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // 포스트 프로세스 볼륨
    private bool isPostProcessingEnabled = false; // 포스트 프로세싱 활성화 여부

    private void Start()
    {
        postProcessVolume.enabled = false;
        GameManager.Instance.OnGuardPostureActivated.AddListener(EnablePostProcessing);
        GameManager.Instance.OnGuardPostureDeactivated.AddListener(DisablePostProcessing);
    }

    private void EnablePostProcessing()
    {
        if (!isPostProcessingEnabled)
        {
            postProcessVolume.enabled = true;
            isPostProcessingEnabled = true;
        }
    }

    private void DisablePostProcessing()
    {
        if (isPostProcessingEnabled)
        {
            postProcessVolume.enabled = false;
            isPostProcessingEnabled = false;
        }
    }
}