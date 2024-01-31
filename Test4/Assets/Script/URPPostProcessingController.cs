using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//URP 포스트 프로세싱 관련
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class URPPostProcessingController : MonoBehaviour
{
    // 포스트 프로세싱 활성화 여부
    private bool isPostProcessingEnabled = false;

    public Volume postProcessingVolume;

    private Vignette _vignette;

    private float currentIntensity = 0f;
    private float _duration = 0.01f;

    //코루틴
    private Coroutine currentCoroutine;

    private void Start()
    {

        postProcessingVolume.enabled = false;

        postProcessingVolume.profile.TryGet(out _vignette);

        //GameManager.Instance.OnGuardPostureActivated.AddListener(EnablePostProcessing);
        //GameManager.Instance.OnGuardPostureDeactivated.AddListener(DisablePostProcessing);
    }


    public void SetVignette()
    {
        _vignette.intensity.value = 0.5f;

    }

    private void EnablePostProcessing()
    {
        if (!isPostProcessingEnabled)
        {
            postProcessingVolume.enabled = true;
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

        if (postProcessingVolume.profile.TryGet(out _vignette))
        {
            currentIntensity = _vignette.intensity.value;

            float targetIntensity = 0.5f;

            while (timer <= duration)
            {
                float progress = timer / duration;
                _vignette.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, progress);

                timer += Time.deltaTime;

                yield return null;
            }
        }
    }

    private IEnumerator ExpandVignette(float duration)
    {

        float timer = 0f;

        if (postProcessingVolume.profile.TryGet(out _vignette))
        {
            currentIntensity = _vignette.intensity.value;
            float targetIntensity = 0f;

            while (timer <= duration)
            {
                float progress = timer / duration;
                _vignette.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, progress);

                timer += Time.deltaTime;
                yield return null;
            }
        }

        if (isPostProcessingEnabled && currentIntensity <= 0.01f)
        {
            postProcessingVolume.enabled = false;
            isPostProcessingEnabled = false;
            currentIntensity = 0f;
        }

    }


}
