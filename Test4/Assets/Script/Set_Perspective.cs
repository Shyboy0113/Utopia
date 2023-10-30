using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Perspective : MonoBehaviour
{
    float fov = 60f;
    float near = 0.3f;
    float far = 1000f;
    

    private void Start()
    {
        
        InitializeCamera();
    }

    void InitializeCamera()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            mainCamera.orthographic = true;  // 원하는 설정으로 초기화
            mainCamera.projectionMatrix = Matrix4x4.Perspective(fov, GetAspectRatio(), near, far);
        }
        else
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다.");
        }
    }

    float GetAspectRatio()
    {
        return Screen.width / (float)Screen.height;
    }
}
