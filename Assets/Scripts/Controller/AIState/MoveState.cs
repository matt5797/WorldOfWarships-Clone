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
        // ������ �̵� �켱
        Vector3 destination;
        Vector3 lookAtPoint;

        float currentTime = 0;

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
            t.autoPilot.SetDestination(destination);
        }

        public override void Update(AIController t)
        {
            base.Update(t);

            // ��� ����Ʈ�� ���� ����� ���� 5�� �� ��ġ�� �Ѵ�.
            t.TargetPoint = t.GetClosestEnemy().PredictionPos(5);

            if (currentTime > t.AttackInterval)
            {
                currentTime = 0;
                if ((t.TargetPoint - t.ship.transform.position).magnitude < t.ship.AttackRange)
                {
                    t.ship.TriggerAbility();
                }
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }

        public override void Exit(AIController t)
        {
            base.Exit(t);
        }
    }

    public class IdleState : State<AIController>
    {
        // ��� ��� ����
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

            // ��� ����Ʈ�� ���� ����� ���� 5�� �� ��ġ�� �Ѵ�.
            t.TargetPoint = t.GetClosestEnemy().PredictionPos(5);
        }
    }

    public class OccupyState : State<AIController>
    {
        Zone zone;
        Vector3 destination;
        float currentTime = 0;
        float currentTime2 = 0;
        float resetDestinationTime = 30f;

        public OccupyState(Zone zone) : base()
        {
            this.zone = zone;
        }
        
        // ���� ���� ����
        public override State<AIController> InputHandle(AIController t)
        {
            return this;
        }

        public override void Enter(AIController t)
        {
            base.Enter(t);
            t.autoPilot.SetActive(true);
            ResetDestination(t);
        }

        public override void Update(AIController t)
        {
            base.Update(t);

            // �� �������� ���� �̻� ��������� ���� ��ġ�� ������ ����
            if ((destination - t.ship.transform.position).magnitude < 10f)
            {
                ResetDestination(t);
            }

            // ���� �ð����� �������� ����
            if (currentTime2 > resetDestinationTime)
            {
                currentTime2 = 0;
                ResetDestination(t);
            }
            else
            {
                currentTime2 += Time.deltaTime;
            }

            // ��� ����Ʈ�� ���� ����� ���� 5�� �� ��ġ�� �Ѵ�.
            t.TargetPoint = t.GetClosestEnemy().PredictionPos(5);

            if (currentTime > t.AttackInterval)
            {
                currentTime = 0;
                if ((t.TargetPoint - t.ship.transform.position).magnitude < t.ship.AttackRange)
                {
                    //t.ship.TriggerAbility();
                    // �ӽ÷� ���� Ʈ����
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        t.ship.ChangeArmament(1);
                        t.ship.TriggerAbility();
                    }
                    else
                    {
                        t.ship.ChangeArmament(2);
                        t.ship.TriggerAbility();
                    }
                    
                    if (UnityEngine.Random.value<0.1f)
                    {
                        t.ship.ChangeArmament(3);
                        t.ship.TriggerAbility();
                    }
                }
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }

        void ResetDestination(AIController t)
        {
            int count = 0;
            do
            {
                count++;
                //destination = zone.transform.position + UnityEngine.Random.insideUnitSphere * zone.radius;
                destination = zone.transform.position + (new Vector3(UnityEngine.Random.Range(-1,1), 0, UnityEngine.Random.Range(-1, 1))).normalized * zone.radius;
            } while (!t.autoPilot.SetDestination(destination) && count<20);
        }
    }

    public class AttackState : State<AIController>
    {
        // ���� Ÿ�� ���� �켱
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