using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace PlayerFolder
{
    [Serializable]
    public class PlayerState
    {
        protected AudioSource audioSource;
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

        public List<AudioClip> audioClips = new List<AudioClip>();

        private AudioClip GetRandomClip()
        {
            var size = audioClips.Count;
            return audioClips[Random.Range(0, size)];
        }

        protected void UpdateAudio()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = GetRandomClip();
                audioSource.Play();
            }
        }
        public PlayerState(){}

        public virtual void InjectInfo(PlayerHandler playerHandler, InputHandler inputHandler, PlayerData playerData,
            StateMachine stateMachine, Rigidbody2D rigidbody2D, Animator animator, AudioSource audioSource)
        {
            //this.stateName = stateName;
            //this.animationBoolName = animationBoolName;
            this.playerHandler = playerHandler;
            this.inputHandler = inputHandler;
            this.playerData = playerData;
            this.stateMachine = stateMachine;
            this.rigidbody2D = rigidbody2D;
            this.animator = animator;
            this.audioSource = audioSource;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void EnterState()
        {
            stateActive = true;
            animationActive = true;
            //Debug.Log("socorro");
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
        }

        public virtual void LeaveState()
        {
            stateActive = false;
            leaveStateEvent.Invoke();
            audioSource.Stop();
        }

        public virtual void FixedUpdateState()
        {
            playerHandler.SetWalk();
            fixedUpdateStateEvent.Invoke();
        }
        
        public virtual void UpdateState()
        {
            playerHandler.UpdateNearestAttachPoint(rigidbody2D.position);
            Debug.Log("bbbbbbb");
            Debug.Log(inputHandler.grapplingRequest);
            Debug.Log(playerHandler.canAttach);
            if (inputHandler.grapplingRequest && playerHandler.canAttach)
            {
                Debug.Log("AAAAAAAAAAA");
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