    
using System;
using UnityEngine;

namespace PlayerFolder
{
    [Serializable]
    public class StateMachine : MonoBehaviour
    {
        private new Rigidbody2D rigidbody2D;
        private Animator animator;
        
        [SerializeField]
        private PlayerData playerData;
        private InputHandler inputHandler;
        private PlayerHandler playerHandler;
        private AudioSource audioSource;
        
        [SerializeField]    
        private PlayerState currentState;

        public JumpState jumpState = new JumpState();
        public WalkState walkState = new WalkState();
        public IdleState idleState = new IdleState();
        public FallingState fallingState = new FallingState();
        public DialogState dialogState = new DialogState();
        public GrappleState grappleState = new GrappleState();
        
        public void ChangeState (PlayerState playerState)
        {
            if(playerState == currentState) return;
            
            currentState.LeaveState();
            currentState = playerState;
            playerState.EnterState();
        }
        private void Initialize(PlayerState playerState)
        {
            if(currentState != null) 
                currentState = playerState;
        }
        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerHandler = GetComponent<PlayerHandler>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            
            jumpState.InjectInfo(playerHandler,inputHandler,playerData,this,rigidbody2D,animator, audioSource);
            walkState.InjectInfo(playerHandler,inputHandler,playerData,this,rigidbody2D,animator, audioSource);
            idleState.InjectInfo(playerHandler,inputHandler,playerData,this,rigidbody2D,animator, audioSource);
            fallingState.InjectInfo(playerHandler,inputHandler,playerData,this,rigidbody2D,animator, audioSource);
            dialogState.InjectInfo(playerHandler,inputHandler,playerData,this,rigidbody2D,animator, audioSource);
            grappleState.InjectInfo(playerHandler,inputHandler,playerData,this,rigidbody2D,animator, audioSource);
        }
        private void Start()
        {
            Initialize(idleState);
        }
        private void Update() => currentState.UpdateState();
        private void FixedUpdate() => currentState.FixedUpdateState();
    }
}