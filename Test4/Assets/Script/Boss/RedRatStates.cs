using System.Net.Mime;
using System.Timers;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
            else if (_rat.CanAttackPlayer(_rat))
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
        
        //그로기 게이지 바 활성화
        public Image groggyGauge_Out;
        public Image groggyGauge;
        
        public RRState_Groggy(Monster monster) : base(monster)
        {
            _rat = (RedRat)monster;
            
            // 이미지를 찾아서 할당(Monobehaviour를 상속받지 않아 생성자에서 진행해야 함)
            // 처음부터 SetActive(False)인 상태에서는 검색도 못 함
            groggyGauge_Out = GameObject.Find("Groggy_Gauge_Out").GetComponent<Image>();
            groggyGauge = GameObject.Find("Groggy_Gauge_In").GetComponent<Image>();
        }

        private float t;
        public override void OnStateEnter()
        {
            _rat.animator.Play("Boss_Groggy");
            _rat._rigidbody.velocity = new Vector3(0,0,0);

            //그로기 진행 바 활성화
            groggyGauge_Out.color = new Color(groggyGauge_Out.color.r, groggyGauge_Out.color.g, groggyGauge_Out.color.b, 1.0f);
            groggyGauge.color = new Color(groggyGauge.color.r, groggyGauge.color.g, groggyGauge.color.b, 1.0f);
            groggyGauge.fillAmount = 1;
            
            //그로기 시간 증가
            t = _rat.currentGroggyValue;
            if (_rat.currentGroggyValue <= 10f) _rat.currentGroggyValue += 0.5f;
            _rat.currentGroggyCount += 1;

        }
         
        public override void OnStateUpdate()
        {
            t -= Time.deltaTime;
            groggyGauge.fillAmount = t/(_rat.currentGroggyValue-0.5f);
            if(t <= 0) _rat.ChangeState(RedRat.RedRatStateEnum.IDLE);
        }

        public override void OnStateExit()
        {
            groggyGauge.fillAmount = 1;
            groggyGauge_Out.color = new Color(groggyGauge_Out.color.r, groggyGauge_Out.color.g, groggyGauge_Out.color.b, 0f);
            groggyGauge.color = new Color(groggyGauge.color.r, groggyGauge.color.g, groggyGauge.color.b, 0f);
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
