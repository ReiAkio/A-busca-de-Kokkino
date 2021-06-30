using System;
using UnityEngine;

namespace PlayerFolder
{
    [Serializable]
    public class FallingState : GroundedState
    {
        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            playerHandler.SetWalk();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (playerHandler.isGrounded)
            {
                stateMachine.ChangeState(stateMachine.idleState);
            }
        }

        protected override void PlayAnimation()
        {
            base.PlayAnimation();
        }
    }
}