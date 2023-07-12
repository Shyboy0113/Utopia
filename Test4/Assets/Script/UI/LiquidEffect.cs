using UnityEngine;
using UnityEngine.UI;

public class LiquidEffect : MonoBehaviour
{
    public Image fillImage;
    public float fillSpeed = 1f;

    private float currentHP;
    private float maxHP;

    private void Start()
    {
        // Initialize current and max HP values
        currentHP = GetCurrentHP();
        maxHP = GetMaxHP();
    }

    private void Update()
    {
        // Update current and max HP values if necessary
        float newCurrentHP = GetCurrentHP();
        float newMaxHP = GetMaxHP();

        if (newCurrentHP != currentHP || newMaxHP != maxHP)
        {
            currentHP = newCurrentHP;
            maxHP = newMaxHP;
        }

        // Calculate fill amount based on current HP and maximum HP
        float fillAmount = Mathf.Clamp01(currentHP / maxHP);

        // Smoothly interpolate the fill amount over time
        float targetFillAmount = Mathf.Lerp(fillImage.fillAmount, fillAmount, Time.deltaTime * fillSpeed);

        // Update the fill amount of the HP window
        fillImage.fillAmount = targetFillAmount;
    }

    private float GetCurrentHP()
    {
        // Implement your logic to get the current HP value
        // Example: return characterStats.currentHP;
        return PlayerManager.Instance.Hp_value;
    }

    private float GetMaxHP()
    {
        // Implement your logic to get the maximum HP value
        // Example: return characterStats.maxHP;

        return PlayerManager.Instance.Mp_value;
    }
}