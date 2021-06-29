﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PlayerFolder
{
    [Serializable]
    public class PlayerState
    {
        protected PlayerHandler playerHandler;
        protected InputHandler inputHandler;
        protected PlayerData playerData;
        protected Rigidbody2D rigidbody2D;
        protected Animator animator;

        [SerializeField]
        private string stateName;
        [SerializeField]
        private string animationBoolName;
        
        protected float startTime;
        protected bool stateActive;
        protected bool animationActive;

        protected StateMachine stateMachine;

        public UnityEvent enterStateEvent;
        public UnityEvent leaveStateEvent;
        public UnityEvent logicUpdateStateEvent;
        public UnityEvent fixedUpdateStateEvent;

        
        
        public PlayerState(){}

        public virtual void InjectInfo(PlayerHandler playerHandler,InputHandler inputHandler,PlayerData playerData,StateMachine stateMachine,Rigidbody2D rigidbody2D,Animator animator)
        {
            //this.stateName = stateName;
            //this.animationBoolName = animationBoolName;
            this.playerHandler = playerHandler;
            this.inputHandler = inputHandler;
            this.playerData = playerData;
            this.stateMachine = stateMachine;
            this.rigidbody2D = rigidbody2D;
            this.animator = animator;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void EnterState()
        {
            stateActive = true;
            animationActive = true;
            
            startTime = Time.time;
            //FixedUpdateState();
            PlayAnimation();
            enterStateEvent?.Invoke();
        }

        protected virtual void PlayAnimation()
        {
            if (animationBoolName != "")
            {
                animator.Play(animationBoolName);
            }
            else
            {
                Debug.Log("no animation name");
            }
        }

        public virtual void LeaveState()
        {
            stateActive = false;
            leaveStateEvent.Invoke();
        }

        public virtual void FixedUpdateState()
        {
            playerHandler.SetWalk();
            fixedUpdateStateEvent.Invoke();
        }
        
        public virtual void UpdateState()
        {
            playerHandler.UpdateNearestAttachPoint(rigidbody2D.position);

            if (inputHandler.grapplingRequest && playerHandler.canAttach)
            {
                stateMachine.ChangeState(stateMachine.grappleState);
            }
            
            if (playerHandler.isTalking)
            {
                stateMachine.ChangeState(stateMachine.dialogState);
            }
            logicUpdateStateEvent.Invoke();
        }

        public virtual void AnimationTriggerReach()
        {
            
        }

        public virtual void AnimationFinished() => animationActive = false;
    }
}