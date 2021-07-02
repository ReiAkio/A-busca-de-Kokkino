using UnityEngine;

namespace PlayerFolder
{
    public abstract class GroundedState : PlayerState
    {
        private float startedCoyoteTime;
        
        protected void StartCoyoteTime() => startedCoyoteTime = Time.time;
        public override void EnterState()
        {
            base.EnterState();
        }
        

        public override void LeaveState()
        {
            base.LeaveState();
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }

        public override void UpdateState()
        {
            if (playerHandler.isGrounded || startedCoyoteTime + playerData.coyoteMaxTime > Time.time)
            {
                
                
                if(!Mathf.Approximately(inputHandler.inputDirection.x,0f))
                {
                    stateMachine.ChangeState(stateMachine.walkState);
                }
                else
                {
                    stateMachine.ChangeState(stateMachine.idleState);
                }
            }
            else
            {
                stateMachine.ChangeState(stateMachine.fallingState);
            }
            
            if (inputHandler.JumpRequest && playerHandler.CanJump())
            {
                stateMachine.ChangeState(stateMachine.jumpState);
            }

            base.UpdateState();
        }

        public override void AnimationTriggerReach()
        {
            base.AnimationTriggerReach();
        }

        public override void AnimationFinished()
        {
            base.AnimationFinished();
        }
    }
}