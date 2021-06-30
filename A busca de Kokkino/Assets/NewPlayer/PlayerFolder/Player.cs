using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerFolder
{
    [RequireComponent(typeof(StateMachine),typeof(InputHandler),typeof(PlayerHandler))]
    public class Player : MonoBehaviour
    {
        private new Transform transform;
        private new Rigidbody2D rigidbody2D;
        private new Collider2D collider2D;
        
        private PlayerHandler playerHandler;
        private InputHandler inputHandler;
        private StateMachine stateMachine;
        
        private void Awake()
        {
            playerHandler = GetComponent<PlayerHandler>();
            inputHandler = GetComponent<InputHandler>();
            stateMachine = GetComponent<StateMachine>();
        }

        private void Start()
        {
            
        }
    }
}