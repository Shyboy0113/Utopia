using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlinkingText : MonoBehaviour
{
    public float blinkInterval = 1.0f; // Adjust the blinking speed here
    public GameObject selectUI;
    
    
    private TMP_Text textComponent;
    private bool isVisible = true;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        InvokeRepeating("ToggleVisibility", blinkInterval, blinkInterval);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            CancelInvoke("ToggleVisibility");
            selectUI.SetActive(true);

        }
    }

    private void ToggleVisibility()
    {
        isVisible = !isVisible;
        textComponent.enabled = isVisible;
    }
}