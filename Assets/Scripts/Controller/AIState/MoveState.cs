using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;
using WOW.BattleShip;
using System;

namespace WOW.Controller
{
    public class MoveState : State<AIController>
    {
        // 목적지 이동 우선
        Vector3 destination;
        Vector3 lookAtPoint;

        public MoveState(Vector3 destination) : base()
        {
            this.destination = destination;
        }
        
        public MoveState(Vector3 destination, Vector3 lookAtPoint) : base()
        {
            this.destination = destination;
            this.lookAtPoint = lookAtPoint;
        }
        
        public override State<AIController> InputHandle(AIController t)
        {
            if (t.autoPilot.isArrived)
            {
                return new IdleState();
            }
            return this;
        }

        public override void Enter(AIController t)
        {
            base.Enter(t);
            t.autoPilot.SetActive(true);
            t.autoPilot.SetDestination(destination, lookAtPoint);
        }

        public override void Update(AIController t)
        {
            base.Update(t);

            // 사격 포인트를 가장 가까운 적의 5초 뒤 위치로 한다.
            t.TargetPoint = t.GetClosestEnemy().PredictionPos(5);
        }

        public override void Exit(AIController t)
        {
            base.Exit(t);
        }
    }

    public class IdleState : State<AIController>
    {
        // 명령 대기 상태
        public override State<AIController> InputHandle(AIController t)
        {
            return this;
        }

        public override void Enter(AIController t)
        {
            base.Enter(t);
        }
        
        public override void Update(AIController t)
        {
            base.Update(t);

            // 사격 포인트를 가장 가까운 적의 5초 뒤 위치로 한다.
            t.TargetPoint = t.GetClosestEnemy().PredictionPos(5);
        }
    }

    public class AttackState : State<AIController>
    {
        // 지정 타겟 공격 우선
        enum AttackType
        {
            HeadOn,
            Bombardment
        }
        
        BattleShipBase target;
        AttackType attackType = AttackType.Bombardment;
        public float pathTime = 0.5f;
        Vector3 destination;

        public AttackState(BattleShipBase target) : base()
        {
            this.target = target;
        }

        public override State<AIController> InputHandle(AIController t)
        {
            if (target==null)
            {
                return new IdleState();
            }
            return this;
        }

        public override void Enter(AIController t)
        {
            base.Enter(t);
            t.autoPilot.enabled = true;
            
            destination = target.transform.position;
            t.StartCoroutine(ReCalculateDestination(t));
        }

        public override void Update(AIController t)
        {
            base.Update(t);
            switch (attackType)
            {
                case AttackType.HeadOn:
                    UpdateHeadOn(t);
                    break;
                case AttackType.Bombardment:
                    UpdateBombardment(t);
                    break;
            }
        }

        private void UpdateBombardment(AIController t)
        {
            throw new NotImplementedException();
        }

        private void UpdateHeadOn(AIController t)
        {
            destination = target.transform.position;
        }

        public override void Exit(AIController t)
        {
            base.Exit(t);
        }

        IEnumerator ReCalculateDestination(AIController t)
        {
            while(target!=null)
            {
                yield return new WaitForSeconds(pathTime);
                t.autoPilot.SetDestination(destination);
            }
        }
    }
}