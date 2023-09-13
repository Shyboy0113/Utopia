using UnityEngine;  
using System.Collections;  
using System;
using UnityEngine.UI;
  
public class Activate_Gauge : MonoBehaviour {  
  
    public Image dialogueGauge; // 원형 게이지 UI 요소를 연결합니다.

    private Animator BossState;
    
    public float GroggyTime = 3.0f;
    private float currentFill = 0.0f;
    private bool isFilling = false;

    private void Start()
    {
        BossState = GameObject.Find("Boss").transform.GetChild(0).GetComponent<Animator>();
        
    }

    void Update()
    {
        if (BossState.GetCurrentAnimatorStateInfo(0).IsName("Boss_Groggy"))
        {
            isFilling = true;
        }
        else
        {
            dialogueGauge.gameObject.SetActive(false);
        }
        
        if (isFilling) // 대화 키를 누르고 있는지 확인합니다.
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentFill = 0f;
                dialogueGauge.gameObject.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                // 게이지를 채우는 로직을 구현합니다.
                currentFill += Time.deltaTime / GroggyTime;
                currentFill = Mathf.Clamp01(currentFill); // 0과 1 사이로 유지
                dialogueGauge.fillAmount = currentFill;
            }

            // Space 키를 놓으면 게이지 초기화 및 비활성화
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ResetFillGauge();
            }

            // 게이지가 100% 이상이면 대화를 시작합니다.
            if (currentFill >= 1.0f)
            {
                StartDialogue(); // 대화 시작 함수 호출
                isFilling = false; // 게이지 채우기 종료
                dialogueGauge.gameObject.SetActive(false); // 게이지 비활성화
                currentFill = 0.0f; // 게이지 초기화
            }
        }
        else
        {
            ResetFillGauge();
        }
    }

    public void ResetFillGauge()
    {
        currentFill = 0.0f; // 게이지 초기화
        dialogueGauge.gameObject.SetActive(false); // 게이지 비활성화
    }

    // 대화를 시작하는 함수를 구현합니다.
    void StartDialogue()
    {
        // 대화 시작 로직을 여기에 추가합니다.
        Debug.Log("대화를 시작합니다.");
    }
    
}  