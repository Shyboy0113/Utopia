using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeColorController : MonoBehaviour
{
    [Header("Target Volume (Global / Weight=1 / Priority 낮게)")]
    [SerializeField] private Volume timeVolume;

    [Header("Blend window at 6/12/18/24 (hours)")]
    [SerializeField, Min(0f)] private float blendWindowHours = 1f; // 경계에서만 부드럽게

    [Header("Tint strength (0=무색, 1=완전적용)")]
    [SerializeField, Range(0f,1f)] private float tintStrength = 0.8f;

    [Header("Colors by slot")]
    [SerializeField] private Color nightNavy = new Color32(59,63,112,255);   // 0~6
    [SerializeField] private Color peach     = new Color32(234,191,157,255); // 6~12
    [SerializeField] private Color orange    = new Color32(221,106,30,255);  // 12~18
    [SerializeField] private Color red       = new Color32(165,53,53,255);   // 18~24

    private ColorAdjustments ca;

    void Awake()
    {
        if (timeVolume && timeVolume.profile) timeVolume.profile.TryGet(out ca);
    }

    void Update()
    {
        if (ca == null) return;

        float h = TimeManager.Instance ? TimeManager.Instance.GetCurrentHour() : 12f;
        Color target = EvaluateColorByHour(h);
        // Color Filter는 곱연산이라 너무 세면 과하게 물듭니다. 흰색과 섞어 세기를 조절.
        ca.colorFilter.value = Color.Lerp(Color.white, target, tintStrength);
    }

    Color EvaluateColorByHour(float h)
    {
        h = Mathf.Repeat(h, 24f);
        float half = Mathf.Max(0.0001f, blendWindowHours * 0.5f);

        // 6시 경계: Night -> Peach
        if (h >= 6f - half && h < 6f + half)
            return LerpSmooth(nightNavy, peach, (h - (6f - half)) / (2f*half));

        // 12시 경계: Peach -> Orange
        if (h >= 12f - half && h < 12f + half)
            return LerpSmooth(peach, orange, (h - (12f - half)) / (2f*half));

        // 18시 경계: Orange -> Red
        if (h >= 18f - half && h < 18f + half)
            return LerpSmooth(orange, red, (h - (18f - half)) / (2f*half));

        // 24/0 경계: Red -> Night (랩 처리)
        if (h >= 24f - half || h < half)
        {
            float local = (h >= 24f - half) ? h - (24f - half) : h + half; // 0~2*half
            return LerpSmooth(red, nightNavy, local / (2f*half));
        }

        // 경계 구간이 아니면 고정 색
        if (h < 6f)   return nightNavy;
        if (h < 12f)  return peach;
        if (h < 18f)  return orange;
        return red;
    }

    static Color LerpSmooth(Color a, Color b, float t)
    {
        t = Mathf.Clamp01(t);
        t = t * t * (3f - 2f * t); // smoothstep
        return Color.Lerp(a, b, t);
    }
}
