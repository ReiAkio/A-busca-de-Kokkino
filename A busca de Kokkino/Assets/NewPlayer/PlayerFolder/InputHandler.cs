
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerFolder
{
    [RequireComponent(typeof(PlayerInput),typeof(Rigidbody2D),typeof(Collider2D))]
    public class InputHandler : MonoBehaviour
    {
        //private PlayerInput playerInput;
        
        public Vector2 inputDirection;
        [SerializeField]
        private bool jumpRequest;

        public bool jumpButton;

        public bool grapplingRequest = false;
        
        public bool JumpRequest
        {
            get
            {
                if (jumpRequest)
                {
                    jumpRequest = false;
                    return true;
                }
                return false;
            }
            set => jumpRequest = value;
        }

        public bool IsNearAPoint;

        public void OnWalk(InputAction.CallbackContext context)
        {
            inputDirection = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            var value = context.ReadValueAsButton();
            jumpRequest = value;
            jumpButton = value;
        }

        public void OnGrapple(InputAction.CallbackContext context)
        {
            Debug.Log("grapple request");
            
            grapplingRequest = !grapplingRequest;
        }
    }
}