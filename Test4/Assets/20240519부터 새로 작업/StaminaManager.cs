using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public Image staminaGauge; // ���¹̳� ������ �̹���
    private RectTransform _gaugeTransform; // ���¹̳� �������� RectTransform
    private Transform _playerTransform; // �÷��̾� ��ü�� Transform
    private Camera _camera; // ī�޶� ��ü

    private Vector3 _screenPos; // �÷��̾��� ȭ�� ��ġ

    void Start()
    {
        _gaugeTransform = staminaGauge.GetComponent<RectTransform>();

        // �÷��̾� ��ü ã��
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        if (_playerTransform == null)
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // ī�޶� ��ü ã��
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (_camera == null)
            _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // �÷��̾��� ȭ�� ��ġ �ʱ�ȭ
        if (_playerTransform != null)
            _screenPos = _camera.WorldToScreenPoint(_playerTransform.position);
    }

    void Update()
    {
        // �÷��̾�� ī�޶� ��ü�� null���� Ȯ���ϰ� ����
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

        // �÷��̾��� ȭ�� ��ġ ����
        if (_playerTransform != null)
            _screenPos = _camera.WorldToScreenPoint(_playerTransform.position);

        // ���¹̳� ������ ��ġ ����
        _gaugeTransform.position = _screenPos + new Vector3(20, 75, 0);

        // ���¹̳� �������� fillAmount �� ����
        staminaGauge.fillAmount = GameManager.Instance.playerStamina / GameManager.Instance.maxStamina;

        // ���¹̳� �������� ���� �� ����
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
