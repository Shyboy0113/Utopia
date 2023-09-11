using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private enum MonsterStateEnum
    {
        IDLE,
        CHASE,
        ATTACK,
        DEATH,
        GROGGY
    }
    private MonsterStateEnum _state;

    private void Start()
    {
        _state = MonsterStateEnum.IDLE;
    }

    private void Update()
    {
        switch (_state)
        {
            case MonsterStateEnum.IDLE:
                break;
            case MonsterStateEnum.CHASE:
                break;
            case MonsterStateEnum.ATTACK:
                break;
            case MonsterStateEnum.DEATH:
                break;
            case MonsterStateEnum.GROGGY:
                break;
            default:
                break;
        }
    }
}