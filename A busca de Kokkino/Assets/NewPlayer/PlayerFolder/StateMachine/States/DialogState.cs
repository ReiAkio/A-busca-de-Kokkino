using System;

namespace PlayerFolder
{
    [Serializable]
    public class DialogState : GroundedState
    {
        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            playerHandler.SetVelocityX(0f);
        }
    }
}