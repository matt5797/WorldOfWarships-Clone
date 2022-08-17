using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOW.Data;

namespace WOW.Controller
{
    public class MoveState : State<AIController>
    {
        Vector3 destination;
        
        public MoveState(Vector3 destination)
        {
            this.destination = destination;
        }
        
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
        }
        public override void LateUpdate(AIController t)
        {
            base.LateUpdate(t);
        }

        public override void Exit(AIController t)
        {
            base.Exit(t);
        }
    }
}