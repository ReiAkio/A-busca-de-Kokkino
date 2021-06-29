using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayerFolder
{
    public class PlayerHandler : MonoBehaviour
    {
        private Player player;
        private InputHandler inputHandler;
        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;

        [SerializeField] private PlayerData playerData;

        #region Serialized data

        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private Transform wallCheck;
        [SerializeField]
        private Transform ceilingCheck;
        
        [SerializeField]
        private float groundCheckRadius = 1f;
        [SerializeField]
        private float wallCheckDistance;

        
        #endregion

        #region player handler variables

        
        
        public float JumpInstantVelocity;
        
        public bool isTouchingWall;
        public bool isTalking;
        public bool isGrounded;
        public float currentSpeed;
        
        
        public List<GameObject> attachablePoints;
        public GameObject nearestAttachablePoint;
        public float maxAttachDistance;
        public bool canAttach;
        public bool isAttached;
        
        #endregion
        
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
            isGrounded = GroundCheck();
            isTouchingWall = WallFrontCheck();
            
        }

        #endregion

        #region Verify functions
        
        private bool CeilCheck() => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius,layerMask);
        private bool GroundCheck() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,layerMask);

        private bool WallFrontCheck()
        {
            var hit = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, layerMask);
            Debug.DrawRay(wallCheck.position, Vector2.right * (facingDirection * wallCheckDistance));
            if (hit)
            {
                Debug.DrawLine(wallCheck.position,hit.point,Color.red);
            }
            return hit;
        } 
        
        private bool WallBackCheck() => Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection, wallCheckDistance, layerMask);
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
            return true;
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
            JumpInstantVelocity = playerData.initialJumpVelocity;
        }
        public void UpdateJump()
        {
            var position = rigidbody2D.position;
            rigidbody2D.position = position + JumpInstantVelocity * Time.deltaTime * Vector2.up;
            JumpInstantVelocity -= playerData.jumpDrag;
        }
    }
}