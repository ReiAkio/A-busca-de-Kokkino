﻿using UnityEngine;

namespace PlayerFolder
{
    [CreateAssetMenu(fileName = "new PlayerData", menuName = "Create new PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public float coyoteMaxTime = 0.5f;
        
        public int maxJumpCount = 2;
        public int currentJumpCount = 0;
        
        public float jumpVelocity = 5f;
        public float speed = 1f;
        public float initialJumpVelocity = 20f;
        public float jumpDrag = 0.5f;
        [Range(0,100f)]
        public float trampolineForce = 20f;
    }
}