using System;
using System.Collections;
using PixelCrushers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // 포스트 프로세스 볼륨
    private bool isPostProcessingEnabled = false; // 포스트 프로세싱 활성화 여부

    private Coroutine currentCoroutine;

    private float currentIntensity = 0f;
    private float _duration = 0.2f;
    
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

        if (currentCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(NarrowVignette(_duration));
    }

    private void DisablePostProcessing()
    {
        if (currentCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(ExpandVignette(_duration));
        
    }

    private IEnumerator NarrowVignette(float duration)
    {
        float timer = 0f;
        Vignette vignette;

        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            currentIntensity = vignette.intensity;
            float targetIntensity = 0.7f;
            
            while (timer <= duration)
            {
                float progress = timer / duration;
                vignette.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, progress);

                timer += Time.deltaTime;
                
                yield return null;
            }
        }
    }

    private IEnumerator ExpandVignette(float duration)
    {
        float timer = 0f;
        Vignette vignette;

        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            currentIntensity = vignette.intensity;
            float targetIntensity = 0f;
            
            while (timer <= duration)
            {
                float progress = timer / duration;
                vignette.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, progress);

                timer += Time.deltaTime;
                yield return null;
            }
        }
        
        if (isPostProcessingEnabled && currentIntensity <= 0.1f)
        {
            postProcessVolume.enabled = false;
            isPostProcessingEnabled = false;
            currentIntensity = 0f;
        }
        
    }

}