using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum E_TimeName { Morning = 6, Afternoon = 12, Evening = 18, Night = 24 }

public class TimeManager : Singleton<TimeManager>
{
    [Header("Clock UI")]
    [SerializeField] private Image timeArrow;
    [SerializeField] private TextMeshProUGUI timeSlotText;

    [Header("Debug UI (Optional)")]
    [SerializeField] private TextMeshProUGUI timeCountText;
    [SerializeField] private TextMeshProUGUI hourCountText;
    [SerializeField] private TextMeshProUGUI dayCountText;

    [Header("Time Settings")]
    [SerializeField] private float secondsPerGameHour = 60f; // 현실 60초 = 게임 1시간

    private float secondsInHour;  // 0~secondsPerGameHour
    private int hour;             // 0~23
    private int day;

    private void Update()
    {
        // 1) 시간 진행
        secondsInHour += Time.deltaTime;
        while (secondsInHour >= secondsPerGameHour)
        {
            secondsInHour -= secondsPerGameHour;
            hour++;
            if (hour >= 24) { hour = 0; day++; }
        }

        // 2) 화살표 회전 (시계 방향: 음수 부호)
        if (timeArrow)
        {
            float hourF = hour + (secondsInHour / secondsPerGameHour);
            float angle = -(hourF * 360f / 24f);
            timeArrow.rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        // 3) 시간대 텍스트
        if (timeSlotText)
        {
            if (hour < (int)E_TimeName.Morning)         timeSlotText.text = "Night";
            else if (hour < (int)E_TimeName.Afternoon)  timeSlotText.text = "Morning";
            else if (hour < (int)E_TimeName.Evening)    timeSlotText.text = "Afternoon";
            else                                        timeSlotText.text = "Evening";
        }

        // 4) 디버그 UI
        if (timeCountText)   timeCountText.text   = $"Time: {secondsInHour:0}";
        if (hourCountText)   hourCountText.text   = $"Hour: {hour}";
        if (dayCountText)    dayCountText.text    = $"Day: {day}";
    }
    
    public float GetCurrentHour()
    {
        // minuteCount(=시간), secondCount(초 누적)를 합쳐 소수 시(hour)로 반환
        return Mathf.Repeat(hour + (float)secondsInHour / secondsPerGameHour, 24f);
    }
    
}
