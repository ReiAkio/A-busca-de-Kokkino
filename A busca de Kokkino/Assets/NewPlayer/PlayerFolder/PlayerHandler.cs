using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerFolder
{
    public class PlayerHandler : MonoBehaviour
    {
        private Player player;
        private InputHandler inputHandler;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;

        public PlayerData playerData;
        
        public LayerMask groundLayerMask;
        public LayerMask mudLayerMask;
        public LayerMask poisonLayerMask;
        public LayerMask trampolineLayerMask;
        
        public Transform groundCheck;
        public Transform wallCheck;
        public Transform ceilingCheck;
        
        public float groundCheckRadius = 1f;
        public float wallCheckDistance = 1f;
        public float jumpInstantVelocity;
        
        public bool isTouchingWall;
        public bool isTalking;
        public float currentSpeed;
        public bool isGrounded;

        public bool inGround;
        public bool inMud;
        public bool inPoison;
        public bool inTrampoline;
        
        public List<GameObject> attachablePoints;
        public GameObject nearestAttachablePoint;
        public float maxAttachDistance;
        public bool canAttach;
        public bool isAttached;

        public UnityEvent touchedMud;
        public UnityEvent touchedTrampoline;
        public UnityEvent touchedPoison;
        
        #region UnityCallBack

        private void Start()
        {
            player = GetComponent<Player>();
            inputHandler = GetComponent<InputHandler>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            VerifyFlip();
            GroundCheck();
            isTouchingWall = WallFrontCheck();
            
        }

        #endregion

        #region Verify functions
        
        private bool CeilCheck() => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius,groundLayerMask);
        private void GroundCheck()
        {

            var lastFrameMud = inMud;
            var lastFramePoison = inPoison;
            var lastFrameTrampoline = inTrampoline;

            
            var position = groundCheck.position;
            inGround = Physics2D.OverlapCircle(position, groundCheckRadius, groundLayerMask);
            inMud = Physics2D.OverlapCircle(position, groundCheckRadius, mudLayerMask);
            if (inMud &&lastFrameMud !=inMud )
            {
                touchedMud.Invoke();
            }
            inPoison = Physics2D.OverlapCircle(position, groundCheckRadius, poisonLayerMask);
            if (inPoison &&lastFramePoison !=inPoison )
            {
                touchedPoison.Invoke();
                LoadSystem.Reload();
            }
            inTrampoline = Physics2D.OverlapCircle(position, groundCheckRadius, trampolineLayerMask);
            if (inTrampoline &&lastFrameTrampoline !=inTrampoline )
            {
                //Debug.Log("fuahsfhadfjgshf");
                rigidbody2D.AddForce(Vector2.up * playerData.trampolineForce ,ForceMode2D.Impulse);
                touchedTrampoline.Invoke();
            }
            isGrounded = inGround || inMud || inPoison;
            
            if (isGrounded)
            {
                //Debug.Log("grounded");
                playerData.currentJumpCount = 0;
            }
        }

        private bool WallFrontCheck()
        {
            var hit = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayerMask);
            Debug.DrawRay(wallCheck.position, Vector2.right * (facingDirection * wallCheckDistance));
            if (hit)
            {
                Debug.DrawLine(wallCheck.position,hit.point,Color.red);
            }
            return hit;
        } 
        
        private bool WallBackCheck() => Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection, wallCheckDistance, groundLayerMask);
        #endregion

        #region SetSpeedFunction
        public void SetVelocityX(float xSpeed) => rigidbody2D.velocity = new Vector2(xSpeed,rigidbody2D.velocity.y);
        public void SetWalk()
        {
            rigidbody2D.velocity = new Vector2(playerData.speed*inputHandler.inputDirection.x,rigidbody2D.velocity.y);
        }

        public void SetAngledWalk()
        {
        }
        public void SetVelocityY(float ySpeed) => rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,ySpeed);

        public void SetVelocity(Vector2 speed) => rigidbody2D.velocity = speed;
        
        public void SetVelocity(float speedX,float speedY) => rigidbody2D.velocity = new Vector2(speedX,speedY);
        #endregion
        
        public float facingDirection = 1f;
        
        public Vector2 currentVelocity;

        private void VerifyFlip()
        {
            if (Mathf.Approximately( inputHandler.inputDirection.x,-facingDirection) )
            {
                facingDirection *= -1;
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }

        public bool CanJump()
        {
            var value = playerData.maxJumpCount > playerData.currentJumpCount;
            Debug.Log(value + " can jump");
            return value;
        }

        public void UpdateNearestAttachPoint(Vector3 PlayerPosition)
        {
            nearestAttachablePoint = null;
            canAttach = false;
            float nearestDistance = maxAttachDistance;
            foreach (var point in attachablePoints)
            {
                float newDistance = (PlayerPosition - point.transform.position).magnitude;
                if (newDistance < nearestDistance)
                {
                    nearestDistance = newDistance;
                    nearestAttachablePoint = point;
                    canAttach = true;
                }
            }
        }
        
        public void StartJump()
        {
            jumpInstantVelocity = playerData.initialJumpVelocity;
        }
        public void UpdateJump()
        {
            var position = rigidbody2D.position;
            rigidbody2D.position = position + jumpInstantVelocity * Time.deltaTime * Vector2.up;
            jumpInstantVelocity -= playerData.jumpDrag;
        }
    }
}