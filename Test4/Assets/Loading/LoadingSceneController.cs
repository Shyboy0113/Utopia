using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : Singleton<LoadingSceneController>
{
    private static string targetScene;
    //로딩 딜레이 최소시간
    public float minimumDelay = 2f;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        //잘못된 Scene 접근이 아닐 경우 호출
        if (!string.IsNullOrEmpty(targetScene))
        {
            StartCoroutine(LoadSceneProcess());
        }
    }

    //로딩 화면으로 넘어가는 함수
    public static void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name cannot be null or empty.");
            return;
        }
        targetScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);
        
        //로딩이 너무 빨리 끝났을 경우를 방지
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0f, 1f, timer/minimumDelay);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            
        }

    }
    
}
