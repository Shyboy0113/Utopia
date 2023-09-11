using UnityEngine;

public class RedRat_IdleState: MonsterBaseState
{
    private RedRat _redRat;

    public RedRat_IdleState(RedRat redRat) : base(redRat)
    {
        _redRat = redRat;
    }


    public override void OnStateEnter()
    {
        Debug.Log("===== Start Idle =====");

    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        Debug.Log("===== End Idle =====");
    }

}

public class RedRat_ChaseState: MonsterBaseState
{
    private RedRat _redRat;

    public RedRat_ChaseState(RedRat redRat) : base(redRat)
    {
        _redRat = redRat;
    }


    public override void OnStateEnter()
    {
        Debug.Log("===== Start Chase =====");   
    }

    public override void OnStateUpdate()
    {
        Debug.Log("===== In Chase =====");   
        // TEST
        _redRat.transform.position = new Vector3(_redRat.transform.position.x - 1, 0, 0);
    }

    public override void OnStateExit()
    {
        Debug.Log("===== End Chase =====");
    }

}

public class RedRat_AttackState: MonsterBaseState
{
    private RedRat _redRat;

    public RedRat_AttackState(RedRat redRat) : base(redRat)
    {
        _redRat = redRat;
    }


    public override void OnStateEnter()
    {
        Debug.Log("===== Start Attack =====");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("===== In Attack =====");
        // TEST
        _redRat.hp -= 1;
    }

    public override void OnStateExit()
    {
        Debug.Log("===== End Attack =====");
    }

}



public class RedRat_DeathState: MonsterBaseState
{
    private RedRat _bot;
    public RedRat_DeathState(RedRat redRat) : base(redRat)
    {
        _redRat = redRat;
    }


    public override void OnStateEnter()
    {
        Debug.Log("===== Start Death =====");
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateExit()
    {
        Debug.Log("===== End Death =====");
    }

}