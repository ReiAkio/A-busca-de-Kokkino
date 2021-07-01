using System;
using UnityEngine;

namespace PlayerFolder
{
    [Serializable]
    public class JumpState : PlayerState
    {
        private bool active;
        private bool stopped;
        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (!inputHandler.jumpButton && !stopped) active = false;

            if (!active)
            {
                active = true;
                stopped = true;
                playerHandler.SetVelocityY(rigidbody2D.velocity.y / 2f);
            }

            if (rigidbody2D.velocity.y < 0f) 
            {
                stateMachine.ChangeState(stateMachine.fallingState);
            }
        }
            
        public override void EnterState()
        {
            base.EnterState();
            playerHandler.SetVelocityY(playerData.jumpVelocity);
            active = true;
            stopped = false;
            UpdateAudio();
        }

        public override void LeaveState()
        {
            base.LeaveState();
            playerHandler.SetVelocityY(0f);
        }
    }
}