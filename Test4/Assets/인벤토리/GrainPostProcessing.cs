using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;
public class GrainPostProcessing : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // 포스트 프로세스 볼륨
    
    private bool isPostProcessingEnabled = false; // 포스트 프로세싱 활성화 여부

    private Coroutine currentCoroutine;

    private float currentIntensity = 0f;
    private float currentFocalLength = 0f;
    
    private float _duration = 0.1f;
    

    private void Start()
    {
        postProcessVolume.enabled = false;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnablePostProcessing();   
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            DisablePostProcessing();
        }
        
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
            // If any coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(ActivateEffects(_duration));
    }

    private void DisablePostProcessing()
    {
        if (currentCoroutine != null)
        {
            // If any coroutine is already running, stop it
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(DeactivateEffects(_duration));
    }

    private IEnumerator ActivateEffects(float duration)
    {
        Debug.Log("ActivateEffects called");

        yield return StartCoroutine(AddGrain(duration));
        yield return StartCoroutine(AdjustDepthOfField(duration));
    }

    private IEnumerator DeactivateEffects(float duration)
    {
        Debug.Log("DeactivateEffects called");

        yield return StartCoroutine(RemoveGrain(duration));
        yield return StartCoroutine(RemoveDepthOfField(duration));

        // Check if both effects are below the threshold before disabling post processing
        if (currentIntensity <= 0.1f)
        {
            postProcessVolume.enabled = false;
            isPostProcessingEnabled = false;
            currentIntensity = 0f;
        }
    }


    private IEnumerator AddGrain(float duration)
    {
        Debug.Log("AddGrain called");
    
        float timer = 0f;
        Grain grain;
    
        if (postProcessVolume.profile.TryGetSettings(out grain))
        {
            currentIntensity = grain.intensity;
            float targetIntensity = 1f;
    
            while (timer <= duration)
            {
                float progress = timer / duration;
                grain.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, progress);
    
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
    
    private IEnumerator RemoveGrain(float duration)
    {
        Debug.Log("RemoveGrain called");
    
        float timer = 0f;
        Grain grain;
    
        if (postProcessVolume.profile.TryGetSettings(out grain))
        {
            currentIntensity = grain.intensity;
            float targetIntensity = 0f;
    
            while (timer <= duration)
            {
                float progress = timer / duration;
                grain.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, progress);
    
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
    
    private IEnumerator AdjustDepthOfField(float duration)
    {
        Debug.Log("AdjustDepthOfField called");

        float timer = 0f;
        DepthOfField depthOfField;

        if (postProcessVolume.profile.TryGetSettings(out depthOfField))
        {
            currentFocalLength = depthOfField.focalLength.value;
            float targetFocalLength = 300f; // Set your desired focal length here

            while (timer <= duration)
            {
                float progress = timer / duration;
                depthOfField.focalLength.value = Mathf.Lerp(currentFocalLength, targetFocalLength, progress);

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    private IEnumerator RemoveDepthOfField(float duration)
    {
        Debug.Log("RemoveDepthOfField called");

        float timer = 0f;
        DepthOfField depthOfField;

        if (postProcessVolume.profile.TryGetSettings(out depthOfField))
        {
            currentFocalLength = depthOfField.focalLength.value;
            float targetFocalLength = 0f; // Set a value far away to essentially disable depth of field

            while (timer <= duration)
            {
                float progress = timer / duration;
                depthOfField.focalLength.value = Mathf.Lerp(currentFocalLength, targetFocalLength, progress);

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
