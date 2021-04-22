// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class PlayerManager : MonoBehaviour
// {
//     public PlayerInput playerInput;
//     public PlayerStateMachine playerStateMachine;
//     public PlayerAnimControler playerAnimControler;
//     public PlayerBehaviourController playerBehaviourController;
//
//     public Action playerStateChanged;
//     public Action playerAnimationChanged;
//     public Action playerBehaviorChanged;
//     
//     public Action updatePlayer;
//
//     public Action collided;
//     public Action unCollided;
//     private void Start()
//     {
//         playerInput = new PlayerInput(this);
//         playerStateMachine = new PlayerStateMachine(this);
//         playerAnimControler = new PlayerAnimControler(this);
//         playerBehaviourController = new PlayerBehaviourController(this);
//     }
//
//     private void OnCollisionEnter2D(Collision2D other)
//     {
//         collided?.Invoke();
//     }
//
//     private void OnCollisionExit2D(Collision2D other)
//     {
//         unCollided?.Invoke();
//     }
//
//     private void Update()
//     {
//         updatePlayer?.Invoke();
//     }
// }
//
// class PlayerBehaviourController
// {
//     
//     public PlayerBehaviourController(PlayerManager playerManager)
//     {
//         
//     }
// }
//
// class PlayerAnimControler
// {
//     public PlayerAnimControler(PlayerManager playerManager)
//     {
//         
//     }
// }
//
// class PlayerStateMachine
// {
//     private PlayerInput playerInput;
//     
//     public bool IsJumping;
//     public bool IsFalling;
//     public bool IsWalking;
//     public bool IsRunning;
//
//     public enum PlayerState
//     {
//         idle,
//         walking,
//         ///...    
//     }
//
//     public PlayerState LastFrameState;
//     public PlayerState CurrentFrameState;
//
//
//     public void updateStateMachine()
//     {
//         
//     }
//     public PlayerStateMachine(PlayerManager playerManager)
//     {
//         playerInput = playerManager.playerInput;
//
//     }
// }
//
// class PlayerInput
// {
//
//     public bool JumpRequest;
//     public bool WalkRequest;
//     public bool RunRequest;
//     public bool 
//
//     private void UpdateInput()
//     {
//         var horizontal = Input.GetAxis("Horizontal");
//         var vertical = Input.GetAxis("Vertical");
//
//         if (Input.GetKey(KeyCode.Backspace) || vertical>= 1)
//         {
//             vertical = 1;
//             JumpRequest = true;
//         }
//         else
//         {
//             JumpRequest = false;
//         }
//
//         if (Input.GetKey(KeyCode.LeftControl))
//         {
//             RunRequest = true;
//         }
//         else
//         {
//             RunRequest = false;
//         }
//         
//     }
//     
//     
//     public PlayerInput(PlayerManager playerManager)
//     {
//         playerManager.updatePlayer += UpdateInput;
//     }
//     
// }
