using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    public enum TimeOfDay
    {
        //24시간 기준으로 카운트
        NIGHT = 18,
        DAWN = 5,
        MORNING = 6,
        DAYTIME = 12,
        TWILIGHT = 17 

    }

    public enum ViewType
    {
        HOUR24 = 0,
        AMPM = 1,
        NONE = 2
    }

    public Canvas Watch;
    public Image Timer_Inline;
    public Image Timer_Saliva;
    public TextMeshProUGUI text;
    
    //일, 시
    private int _currentDays;
    public int CurrentDays
    {
        get => _currentDays;
        set => _currentDays = value;
            
    }

    //스토리 진행 중엔 시간 측정을 멈춤
    private bool _isMeasuring;
    public bool IsMeasuring
    {
        get => _isMeasuring;
        set => _isMeasuring = value;
    }
    
    //카운터
    private int _currentHours;
    private float _timeCounter;
    
    //enum 상태
    public TimeOfDay currentTimeOfDay;
    public ViewType currentViewType;
    
    private void Awake()
    {
        //TODO: 세이브파일에서 파싱해서 데이터 저장하는 식으로 전부 바꿔줘야 됨.
        currentTimeOfDay = TimeOfDay.NIGHT; // 시작 시간 설정
        currentViewType = ViewType.HOUR24;
        
        //TODO: 세이브파일에서 파싱해와서 카운트까지 계산해야 함
        _timeCounter = 0.0f;
        _currentHours = 0;
        CurrentDays = 0; //이건 프로퍼티라 여기서 넣은 값이 바로 set됨

    }

    private void Start()
    {
        _isMeasuring = true;
    }

    private void Update()
    {
        if (_isMeasuring is true)
        {
            //시간 표시 ( 24시간, AM/PM제, 표시 안 함)
            ViewTime();
            //시간 측정
            TimeCount();
        }

    }
    
    private void TimeCount(){
    
        // 시간 업데이트
        _timeCounter += Time.deltaTime;

        float rotationPerHour = 15.0f;
        
        float Saliva_Rotate = (_currentHours + (_timeCounter / 60.0f)) * rotationPerHour;
        
        //Debug.Log(Saliva_Rotate);
        
        Timer_Saliva.transform.rotation = Quaternion.Euler(0, 0, (-1) * Saliva_Rotate);
        
        if (_timeCounter >= 60.0f)
        {
            _currentHours += 1;
            _timeCounter = 0;
            
            // 시간대 갱신
            UpdateTimeOfDay();
        }
        
        // 24시간이 지나면 다음 날로 초기화
        if (_currentHours >= 24)
        {
            ChangeDayCount();
        }
        
    }
    
    private void UpdateTimeOfDay()
    {
        // 시간대를 업데이트하는 로직을 여기에 구현
        // timeCounter를 기반으로 시간대를 설정할 수 있습니다.
        // 예: 일출, 아침, 낮, 저녁, 밤 등에 따라 currentTime 값을 설정

        if (0 <= _currentHours && _currentHours < (int)TimeOfDay.DAWN)
        {
            Timer_Inline.color = Color.black;
        }
        else if ((int)TimeOfDay.DAWN <= _currentHours && _currentHours < (int)TimeOfDay.MORNING)
        {
            Timer_Inline.color = new Color(203, 166, 206);
        }
        else if ((int)TimeOfDay.MORNING <= _currentHours && _currentHours < (int)TimeOfDay.DAYTIME)
        {
            Timer_Inline.color = Color.yellow;
        }
        else if ((int)TimeOfDay.DAYTIME <= _currentHours && _currentHours < (int)TimeOfDay.TWILIGHT)
        {
            Timer_Inline.color = new Color(54, 136, 174);
        }
        else if ((int)TimeOfDay.TWILIGHT <= _currentHours && _currentHours < (int)TimeOfDay.NIGHT)
        {
            Timer_Inline.color = new Color(255, 125, 0);
        }
        else if ((int)TimeOfDay.NIGHT <= _currentHours && _currentHours < 24)
        {
            Timer_Inline.color = Color.black;
        }
        
    }

    public void ViewTime()
    {
        switch (currentViewType)
        {
            case ViewType.HOUR24:
                // 24시간 형식으로 표시
                // 예: 12:30
                
                text.text = $"{_currentHours} : 00" ;
                
                break;
            
            case ViewType.AMPM:
                // AM/PM 형식으로 표시
                // 예: 12:30 AM
                
                if (0 <= _currentHours && _currentHours <= 11) text.text = $"AM {_currentHours} : 00";
                else if (12 == _currentHours) text.text = text.text = $"PM {_currentHours} : 00";
                else if(13 <=_currentHours && _currentHours <= 23) text.text = $"PM {_currentHours %12} : 00";
                
                break;
            
            case ViewType.NONE:
                // 표시하지 않음
                // 예: 12:30
                
                text.text = "";
                
                break;
        }
    }
    
    public void ChangeDayCount()
    {
        _currentDays += 1;
        _currentHours = 0;
        _timeCounter = 0.0f;
    }

    public void ChangeToDawn()
    {
        currentTimeOfDay = TimeOfDay.DAWN;
        _currentHours = (int)TimeOfDay.DAWN;
        _timeCounter = (int)currentTimeOfDay * 60.0f;

    }

    public void ChangeToMorning()
    {
        currentTimeOfDay = TimeOfDay.MORNING;
        _currentHours = (int)TimeOfDay.MORNING;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }

    public void ChangeToDaytime()
    {
        currentTimeOfDay = TimeOfDay.DAYTIME;
        _currentHours = (int)TimeOfDay.DAYTIME;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }

    public void ChangeToTwilight()
    {
        currentTimeOfDay = TimeOfDay.TWILIGHT;
        _currentHours = (int)TimeOfDay.TWILIGHT;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }

    public void ChangeToNight()
    {
        currentTimeOfDay = TimeOfDay.NIGHT;
        _currentHours = (int)TimeOfDay.NIGHT;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }
    
}
