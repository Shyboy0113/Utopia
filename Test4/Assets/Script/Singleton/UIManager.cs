using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject PauseMenu;
    
    public RectTransform topPanel;
    public RectTransform bottomPanel;
    
    public float animationDuration = 1.0f;
    public float desiredNarrowedHeight = 100.0f;
    
    //코루틴
    private Coroutine narrowCoroutine;
    
    private void Start()
    {
        // Subscribe to guard posture events
        //MoveManager.Instance.OnGuardPostureActivated.AddListener(NarrowUIPanels);
        //MoveManager.Instance.OnGuardPostureDeactivated.AddListener(ExpandUIPanels);
        
        ResetAllMenu();
    }
    public void ResetAllMenu()
    {
        PauseMenu.gameObject.SetActive(false);
    }

    public void ActivatePauseMenu(bool pauseState)
    {
        PauseMenu.gameObject.SetActive(pauseState);
    }
    
    private void NarrowUIPanels()
    {
        if (narrowCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(narrowCoroutine);
        }

        narrowCoroutine = StartCoroutine(NarrowPanelsOverTime(animationDuration));
    }

    private void ExpandUIPanels()
    {
        if (narrowCoroutine != null)
        {
            // If the narrow coroutine is running, stop it to interrupt the narrowing animation
            StopCoroutine(narrowCoroutine);
        }

        // Determine the initial and target sizes for expanding
        Vector2 initialSize = topPanel.sizeDelta;
        Vector2 targetSize = new Vector2(initialSize.x,150f /* Set your desired expanded height here */);

        // Start the coroutine to expand the panels
        StartCoroutine(ExpandPanelsOverTime(initialSize, targetSize, animationDuration));
    }
    
    private IEnumerator NarrowPanelsOverTime(float duration)
    {
        float timer = 0f;
        Vector2 initialSize = topPanel.sizeDelta;
        Vector2 targetSize = new Vector2(initialSize.x, desiredNarrowedHeight);

        while (timer < duration)
        {
            float progress = timer / duration;
            topPanel.sizeDelta = Vector2.Lerp(initialSize, targetSize, progress);
            bottomPanel.sizeDelta = Vector2.Lerp(initialSize, targetSize, progress);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final size is set exactly at the end
        topPanel.sizeDelta = targetSize;
        bottomPanel.sizeDelta = targetSize;
    }
    
    private IEnumerator ExpandPanelsOverTime(Vector2 initialSize, Vector2 targetSize, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            float progress = timer / duration;
            topPanel.sizeDelta = Vector2.Lerp(initialSize, targetSize, progress);
            bottomPanel.sizeDelta = Vector2.Lerp(initialSize, targetSize, progress);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final size is set exactly at the end
        topPanel.sizeDelta = targetSize;
        bottomPanel.sizeDelta = targetSize;
    }
    
}
