using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState
{
    protected Monster monster;

    public MonsterBaseState(Monster monster)
    {
        this.monster = monster;
    }

    // 상태에 들어왔을 때 한번 실행
    public virtual void OnStateEnter() { }

    // 상태에 있을 때 계속 실행
    public virtual void OnStateUpdate() { }
    // 상태를 빠져나갈 때 한번 실행
    public virtual void OnStateExit() { }
}