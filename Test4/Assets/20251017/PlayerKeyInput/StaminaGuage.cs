using UnityEngine;
using UnityEngine.UI;

public class StaminaGuage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image gaugeCircle;
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCam;

    [Header("Offsets")]
    [SerializeField] private Vector3 worldOffset = new();

    [Header("Guage Activation Thresholds")]
    [SerializeField, Range(0.9f, 1f)] private float hideThreshold = 0.999f;
    [SerializeField, Range(0.9f, 1f)] private float showThreshold = 0.995f;
    [SerializeField, Range(0, 0.3f)] private float changeColorThreshold = 0.3f; 

    private bool _isActive;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        mainCam = Camera.main;
    }

    private void Start()
    {
        SetActiveGauge(false);
    }

    private void LateUpdate()
    {
        if (!gaugeCircle || !player || !mainCam) return;

        Vector3 worldPos = player.position + worldOffset;

        gaugeCircle.rectTransform.position = worldPos;

        float maxStamina = Mathf.Max(0.0001f, GameManager.Instance.ReturnMaxStamina()); // 0 분모 방지
        float curStamina = Mathf.Max(0f, GameManager.Instance.ReturnStamina());         // 음수 방지
        float ratio = Mathf.Clamp01(curStamina / maxStamina);

        // 게이지 채우기
        gaugeCircle.fillAmount = ratio;

        // 3) 표시/숨김 (히스테리시스)
        if (_isActive)
        {
            if (ratio <= changeColorThreshold)
                SetGuageColor(Color.red);
            else
                SetGuageColor(Color.white);

            // 이미 표시 중일 때: 거의 가득 찼으면 숨김
            if (ratio >= hideThreshold)
                SetActiveGauge(false);
        }
        else
        {
            // 숨겨진 상태일 때: 조금이라도 닳았으면 표시
            if (ratio <= showThreshold)
                SetActiveGauge(true);
        }


    }

    public void SetActiveGauge(bool active)
    {
        if (_isActive == active) return;
        
        _isActive = active;

        if (gaugeCircle) gaugeCircle.gameObject.SetActive(active);
        
    }

    public void SetGuageColor(Color color)
    {
        gaugeCircle.color = color;
    }

}
