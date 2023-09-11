using System.Timers;
using UnityEngine;

namespace RedRatStates
{
    public class RRState_Chase : MonsterBaseState
    {
        private RedRat _rat;

        public RRState_Chase(Monster monster) : base(monster)
        {
            _rat = (RedRat)monster;
        }

        public override void OnStateEnter()
        {
            _rat.animator.Play("Boss_Chase");
        }
        public override void OnStateUpdate()
        {
            _rat.Flip();
            _rat._rigidbody.velocity = new Vector2(
                (_rat.m_FacingRight ? 1 : -1) * _rat.chargeSpeed, 0);

            if (_rat.CanSeePlayer(_rat) is false)
            {
                _rat.ChangeState(RedRat.RedRatStateEnum.IDLE);
            }

            if (_rat.CanAttackPlayer(_rat))
            {
                _rat.ChangeState(RedRat.RedRatStateEnum.CHARGE);
            }
        }
    }

    public class RRState_Idle : MonsterBaseState
    {
        private RedRat _rat;
        public RRState_Idle(Monster monster) : base(monster)
        {
            _rat = (RedRat)monster;
        }

        public override void OnStateEnter()
        {
            _rat.animator.Play("Boss_Idle");
            _rat._rigidbody.velocity = new Vector3(0f,0f,0f);
        }

        public override void OnStateUpdate()
        {
            if (_rat.CanSeePlayer(_rat))
            {
                _rat.ChangeState(RedRat.RedRatStateEnum.CHASE);
            }
        }
    }

    public class RRState_Charge : MonsterBaseState
    {
        private RedRat _rat;
        public RRState_Charge(Monster monster) : base(monster)
        {
            _rat = (RedRat)monster;
        }

        private float t;
        public override void OnStateEnter()
        {
            _rat.animator.Play("Boss_Charge");
            Debug.Log(("On Attack") + t);
            t = 2; 
        }

        public override void OnStateUpdate()
        {
            t -= Time.deltaTime;
            if(t <= 0) _rat.ChangeState(RedRat.RedRatStateEnum.RUSH);
        }
    }

    public class RRState_Rush : MonsterBaseState
    {
        private const float SPEED = 20;
        private RedRat _rat;
        public RRState_Rush(Monster monster) : base(monster)
        {
            _rat = (RedRat)monster;
        }

        private float t;
        public override void OnStateEnter()
        {
            _rat.animator.Play("Boss_Rush");
            _rat._rigidbody.velocity = Vector2.right * ((_rat.m_FacingRight ? 1 : -1) * SPEED);
            t = 2f;
        }
        
        public override void OnStateUpdate()
        {
            t -= Time.deltaTime;
            if(t <= 0) _rat.ChangeState(RedRat.RedRatStateEnum.IDLE);
        }
    }
    
    public class RRState_Groggy : MonsterBaseState
    {
        private RedRat _rat;
        public RRState_Groggy(Monster monster) : base(monster)
        {
            _rat = (RedRat)monster;
        }
        
        private float t;
        public override void OnStateEnter()
        {
            _rat.animator.Play("Boss_Groggy");
            _rat._rigidbody.velocity = new Vector3(0,0,0);
            
            t = _rat.currentGroggyValue;
            if (_rat.currentGroggyValue <= 10f) _rat.currentGroggyValue += 0.5f;
            _rat.currentGroggyCount += 1;

        }
        
        public override void OnStateUpdate()
        {
            t -= Time.deltaTime;
            if(t <= 0) _rat.ChangeState(RedRat.RedRatStateEnum.IDLE);
        }
    }
    
    public class RRState_Death : MonsterBaseState
    {
        private RedRat _rat;
        public RRState_Death(Monster monster) : base(monster)
        {
            _rat = (RedRat)monster;
        }

        public override void OnStateEnter()
        {
            _rat.animator.Play("Boss_Death");
            _rat._rigidbody.velocity = new Vector3(0,0,0);
            
        }
        
    }
}
