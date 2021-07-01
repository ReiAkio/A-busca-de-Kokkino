using System;

namespace PlayerFolder
{
    [Serializable]
    public class WalkState : GroundedState
    {
        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            
        }

        public override void UpdateState()
        {
            base.UpdateState();
            UpdateAudio();
        }
    }
}