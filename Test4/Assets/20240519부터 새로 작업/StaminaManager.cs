using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public Image staminaGauge; // 스태미나 게이지 이미지
    private RectTransform _gaugeTransform; // 스태미나 게이지의 RectTransform
    private Transform _playerTransform; // 플레이어 객체의 Transform
    private Camera _camera; // 카메라 객체

    private Vector3 _screenPos; // 플레이어의 화면 위치

    void Start()
    {
        _gaugeTransform = staminaGauge.GetComponent<RectTransform>();

        // 플레이어 객체 찾기
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        if (_playerTransform == null)
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // 카메라 객체 찾기
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (_camera == null)
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // 플레이어의 화면 위치 초기화
        if (_playerTransform != null)
            _screenPos = _camera.WorldToScreenPoint(_playerTransform.position);
    }

    void Update()
    {
        // 플레이어와 카메라 객체가 null인지 확인하고 갱신
        if (_playerTransform == null)
        {
            _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            if (_playerTransform == null)
                _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if (_camera == null)
        {
            _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (_camera == null)
                _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        // 플레이어의 화면 위치 갱신
        if (_playerTransform != null)
            _screenPos = _camera.WorldToScreenPoint(_playerTransform.position);

        // 스태미나 게이지 위치 갱신
        _gaugeTransform.position = _screenPos + new Vector3(20, 75, 0);

        // 스태미나 게이지의 fillAmount 값 갱신
        staminaGauge.fillAmount = GameManager.Instance.playerStamina / GameManager.Instance.maxStamina;

        // 스태미나 게이지의 알파 값 조정
        if (staminaGauge.fillAmount >= 0.999999999f)
        {
            Color newColor = staminaGauge.color; // Store the color in a variable
            newColor.a = 0f; // Modify the alpha value
            staminaGauge.color = newColor; // Assign the modified color back
        }
        else
        {
            Color newColor = staminaGauge.color; // Store the color in a variable
            newColor.a = 1f; // Modify the alpha value
            staminaGauge.color = newColor; // Assign the modified color back
        }
    }
}
