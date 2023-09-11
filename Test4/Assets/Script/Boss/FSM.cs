using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private MonsterBaseState _currentState;
    
    public FSM(MonsterBaseState initState)
    {
        _currentState = initState;
        ChangeState(_currentState);
    }

    public void ChangeState(MonsterBaseState nextState)
    {
        if(nextState == _currentState)
            return;

        if (_currentState != null)
        {
            _currentState.OnStateExit();
        }

        _currentState = nextState;
        _currentState.OnStateEnter();
    }

    public void UpdateState()
    {
        _currentState?.OnStateUpdate();
    }
}