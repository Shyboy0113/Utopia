using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject PauseMenu;
    
    public RectTransform topPanel;
    public RectTransform bottomPanel;
    
    public float animationDuration = 0.1f;
    public float desiredNarrowedHeight = 100.0f;

    
    //코루틴
    private Coroutine narrowCoroutine;
    
    private void Start()
    {
        // Subscribe to guard posture events
        //GameManager.Instance.OnGuardPostureActivated.AddListener(NarrowUIPanels);
        //GameManager.Instance.OnGuardPostureDeactivated.AddListener(ExpandUIPanels);
        

    }
    public void ResetAllMenu()
    {
        PauseMenu.gameObject.SetActive(false);
    }

    public void ActivatePauseMenu(bool pauseState)
    {
        PauseMenu.gameObject.SetActive(pauseState);
    }
    
    public void NarrowUIPanels()
    {
        if (narrowCoroutine != null)
        {
            // If the narrow coroutine is already running, stop it
            StopCoroutine(narrowCoroutine);
        }

        narrowCoroutine = StartCoroutine(NarrowPanelsOverTime(animationDuration));
    }

    public void ExpandUIPanels()
    {
        if (narrowCoroutine != null)
        {
            // If the narrow coroutine is running, stop it to interrupt the narrowing animation
            StopCoroutine(narrowCoroutine);
        }

        // Start the coroutine to expand the panels
        StartCoroutine(ExpandPanelsOverTime(animationDuration));
    }
    
    private IEnumerator NarrowPanelsOverTime(float duration)
    {
        float timer = 0f;
        
        Vector2 initialPosition_Top = topPanel.anchoredPosition;
        Vector2 initialPosition_Bottom = bottomPanel.anchoredPosition;
        
        Vector2 targetPosition_Top = new Vector2(initialPosition_Top.x, 270f );
        Vector2 targetPosition_Bottom = new Vector2(initialPosition_Bottom.x, -270f );
        
        while (timer < duration)
        {
            float progress = timer / duration;
            topPanel.anchoredPosition = Vector2.Lerp(initialPosition_Top, targetPosition_Top, progress);
            bottomPanel.anchoredPosition = Vector2.Lerp(initialPosition_Bottom, targetPosition_Bottom, progress);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set exactly at the end
        topPanel.anchoredPosition = targetPosition_Top;
        bottomPanel.anchoredPosition = targetPosition_Bottom;
    }
    
    private IEnumerator ExpandPanelsOverTime(float duration)
    {
        float timer = 0f;
        
        Vector2 initialPosition_Top = topPanel.anchoredPosition;
        Vector2 initialPosition_Bottom = bottomPanel.anchoredPosition;
        
        Vector2 targetPosition_Top = new Vector2(initialPosition_Top.x, 540f );
        Vector2 targetPosition_Bottom = new Vector2(initialPosition_Bottom.x, -540f );
        
        while (timer < duration)
        {
            float progress = timer / duration;
            topPanel.anchoredPosition = Vector2.Lerp(initialPosition_Top, targetPosition_Top, progress);
            bottomPanel.anchoredPosition = Vector2.Lerp(initialPosition_Bottom, targetPosition_Bottom, progress);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set exactly at the end
        topPanel.anchoredPosition = targetPosition_Top;
        bottomPanel.anchoredPosition = targetPosition_Bottom;
    }
    
}
