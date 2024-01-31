using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public GameObject heartPrefab; // 하트 프리팹을 연결해줘
    public Transform heartsParent; // 하트들이 표시될 부모 객체를 연결해줘
    private List<Image> heartImages = new List<Image>();
    
    public Image staminaGauge;
    public Camera camera;
    
    private RectTransform _gaugeTransform;
    private Transform _playerTransform;

    private Vector3 screenPos;
    void Start()
    {
        GameManager.Instance._playerHp = 5; // 시작할 때의 체력 설정
        UpdateHearts();
        
        _gaugeTransform = staminaGauge.GetComponent<RectTransform>();
        
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        if (_playerTransform is null)
            _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        screenPos = camera.WorldToScreenPoint(_playerTransform.position);
    }

    void Update()
    {
        if (heartImages.Count != GameManager.Instance._playerHp) UpdateHearts();

        //staminaGauge.fillAmount = GameManager.Instance._playerStamina / GameManager.Instance._staminaMax;

        if (camera is null)
        {
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();

            if (camera is null)
                camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        if(_playerTransform is null){
            _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            if (_playerTransform is null)
                _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if (_playerTransform != null)
            screenPos = camera.WorldToScreenPoint(_playerTransform.position);
        
        
        _gaugeTransform.position = screenPos + new Vector3(50,50,0);
        
        if (staminaGauge.fillAmount >= 0.999999999f)
        {
            Color newColor = staminaGauge.color;  // Store the color in a variable
            newColor.a = 0f;  // Modify the alpha value
            staminaGauge.color = newColor;  // Assign the modified color back
            //Debug.Log("Setting Alpha to 0");
        }
        else
        {
            Color newColor = staminaGauge.color;  // Store the color in a variable
            newColor.a = 1f;  // Modify the alpha value
            staminaGauge.color = newColor;  // Assign the modified color back
            //Debug.Log("Setting Alpha to 1");
        }
    }

    private void UpdateHearts()
    {
        // 기존의 하트 이미지들 삭제
        foreach (Image heart in heartImages)
        {
            Destroy(heart.gameObject);
        }
        heartImages.Clear();

        // 새로운 체력에 맞게 하트 이미지들 생성
        for (int i = 0; i < GameManager.Instance._playerHp; i++)
        {
            // 각 하트의 위치를 옆으로 이동
            float xOffset = i * 100f; // 각 하트 간의 간격을 조절
            GameObject heartObj = Instantiate(heartPrefab, heartsParent);
            RectTransform heartRect = heartObj.GetComponent<RectTransform>();
            heartRect.anchoredPosition = new Vector2(xOffset, 0f);
            Image heartImage = heartObj.GetComponent<Image>();
            heartImages.Add(heartImage);
        }
    }




}