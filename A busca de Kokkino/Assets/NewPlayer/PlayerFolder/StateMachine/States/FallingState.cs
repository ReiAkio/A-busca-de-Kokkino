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

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void LeaveState()
        {
            base.LeaveState();
            UpdateAudio();
        }

        protected override void PlayAnimation()
        {
            base.PlayAnimation();
        }
    }
}