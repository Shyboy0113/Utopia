using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    //일, 시, 분 (초 단위로는 표현하지 않음)
    private int _currentDays;
    public int CurrentDays
    {
        get => _currentDays;
        set => _currentDays = value;
            
    }
    
    private int _currentHours;
    private int _currentMinutes;
    
    //enum 상태
    public TimeOfDay currentTimeOfDay;
    public ViewType currentViewType;
    
    //카운터
    private float _timeCounter;

    //난이도에 따른 하루 시간(기준 : 초)
    private float _secondsPerDay; 

    private void Awake()
    {
        //TODO: 세이브파일에서 파싱해서 데이터 저장하는 식으로 전부 바꿔줘야 됨.
        currentTimeOfDay = TimeOfDay.NIGHT; // 시작 시간 설정
        currentViewType = ViewType.HOUR24;
        
        //TODO: 세이브파일에서 파싱해와서 카운트까지 계산해야 함
        _timeCounter = 0.0f;
        CurrentDays = 0; //이건 프로퍼티라 여기서 넣은 값이 바로 set됨

    }

    private void Start()
    {
        // 난이도(분) x 60초 카운트
        _secondsPerDay = DifficultyManager.Instance.standardOfDay * 60.0f;
        
    }

    private void Update()
    {
        //시간 표시 ( 24시간, AM/PM제, 표시 안 함)
        ViewTime();
        
        //시간 측정
        TimeCount();
        
    }
    
    private void TimeCount(){
    
        // 시간 업데이트
        _timeCounter += Time.deltaTime;
        
        // 시간대 갱신
        UpdateTimeOfDay();
        
        // 24시간이 지나면 다음 날로 초기화
        if (_timeCounter >= _secondsPerDay)
        {
            ChangeDayCount();
        }
        
    }
    
    private void UpdateTimeOfDay()
    {
        
        // 시간대를 업데이트하는 로직을 여기에 구현
        // timeCounter를 기반으로 시간대를 설정할 수 있습니다.
        // 예: 일출, 아침, 낮, 저녁, 밤 등에 따라 currentTime 값을 설정
    }

    public void ViewTime()
    {
        switch (currentViewType)
        {
            case ViewType.HOUR24:
                break;
            case ViewType.AMPM:
                break;
            case ViewType.NONE:
                break;
        }
    }
    
    public void ChangeDayCount()
    {
        _currentDays += 1;
        _timeCounter = 0.0f;
    }

    public void ChangeToDawn()
    {
        currentTimeOfDay = TimeOfDay.DAWN;
        _timeCounter = (int)currentTimeOfDay * 60.0f;

    }

    public void ChangeToMorning()
    {
        currentTimeOfDay = TimeOfDay.MORNING;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }

    public void ChangeToDaytime()
    {
        currentTimeOfDay = TimeOfDay.DAYTIME;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }

    public void ChangeToTwilight()
    {
        currentTimeOfDay = TimeOfDay.TWILIGHT;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }

    public void ChangeToNight()
    {
        currentTimeOfDay = TimeOfDay.NIGHT;
        _timeCounter = (int)currentTimeOfDay * 60.0f;
        
    }

    
    
}
