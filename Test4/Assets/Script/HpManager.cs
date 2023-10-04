using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public GameObject heartPrefab; // 하트 프리팹을 연결해줘
    public Transform heartsParent; // 하트들이 표시될 부모 객체를 연결해줘
    private List<Image> heartImages = new List<Image>();
    
    public Image staminaGauge;

    void Start()
    {
        GameManager._playerHp = 5; // 시작할 때의 체력 설정
        UpdateHearts();
    }

    void Update()
    {
        if(heartImages.Count != GameManager._playerHp) UpdateHearts();
        
        staminaGauge.fillAmount = GameManager._playerStamina / GameManager._staminaMax;
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
        for (int i = 0; i < GameManager._playerHp; i++)
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