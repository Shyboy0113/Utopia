using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public float blinkInterval = 1.0f; // Adjust the blinking speed here

    private Text textComponent;
    private bool isVisible = true;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        InvokeRepeating("ToggleVisibility", blinkInterval, blinkInterval);
    }

    private void ToggleVisibility()
    {
        isVisible = !isVisible;
        textComponent.enabled = isVisible;
    }
}