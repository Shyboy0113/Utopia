using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseState
{
    private Monster _monster;

    protected MonsterBaseState(Monster monster)
    {
        _monster = monster;
    }

    // 상태에 들어왔을 때 한번 실행
    public abstract void OnStateEnter();
    // 상태에 있을 때 계속 실행
    public abstract void OnStateUpdate();
    // 상태를 빠져나갈 때 한번 실행
    public abstract void OnStateExit();
}