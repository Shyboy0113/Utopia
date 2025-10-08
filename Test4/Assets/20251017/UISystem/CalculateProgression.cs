using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateProgression : MonoBehaviour
{
    public Slider progressSlider;

    [SerializeField] private Transform leftTransform;
    [SerializeField] private Transform rightTransform;

    private float _targetDistance;
    private float _playerDistance;
    
    private Transform _playerTransform;

    void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Start()
    {
        _targetDistance = Vector3.Distance(leftTransform.position, rightTransform.position);
    }

    void Update()
    {
        Vector3 xVector = new Vector3(_playerTransform.position.x,0,0); // 플레이어의 위치에서 x좌표만 받아온 벡터

        if (xVector.x >= rightTransform.position.x) // 플레이어의 위치가 오른쪽 기준점을 넘겼을 때
        {
            progressSlider.value = 1f;
        }
        else if (xVector.x <= leftTransform.position.x) // 플레이어의 위치가 왼쪽 기준점을 넘겼을 때
        {
            progressSlider.value = 0f;
        }
        else
        {
            _playerDistance = Vector3.Distance(leftTransform.position, xVector);
            progressSlider.value = _playerDistance / _targetDistance;
        }

    }
}
