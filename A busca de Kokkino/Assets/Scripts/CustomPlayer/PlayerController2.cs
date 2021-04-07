using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    //private AnimationType currentAnimation;

    private bool isLama;
    private bool isGrounded;
    private bool isPoison;
    private bool isTrampolim;

    public Transform feetPos;

    public Rigidbody2D rb;

    public float jumpForce;
    public float checkRadius;

    public LayerMask whatIsGround;
    public LayerMask whatIsLama;
    public LayerMask whatIsPoison;
    public LayerMask whatIsTrampolim;

    [Header("Custom Properites for PlayerController")]
    public float JumpForce = 50;
    public float Speed = 20;
    public float RunningSpeed = 40;
    public int RemainJump = 0;
    public int MaxJump = 2;
    public float JumpingGratityScale = 8;
    public float DefaultGratityScale = 12;
    public float HorizontalMovement;
    public float CurrentSpeed;

    [Header("Player movement status")]
    public bool IsJumping;
    public bool IsFalling;
    public bool IsWalking;
    public bool IsRunning;
    public bool IsGrounded
    {
        get { return GroudUnderPlayer.Count > 0; }
    }
    public bool IsIdle
    {
        get { return IsGrounded && !IsJumping && !IsFalling && !IsWalking && !IsRunning; }
    }
    
    public List<GameObject> GroudUnderPlayer = new List<GameObject>(); 
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, -Vector2.up,new ContactFilter2D(),results);
        foreach (RaycastHit2D hit2D in results)
        {
            if (hit2D.collider ==  other.collider)
            {
                CollidableObject collidableObject = other.gameObject.GetComponent<CollidableObject>();
                if (collidableObject)
                {
                    IsFalling = false;
                    RemainJump = MaxJump;
                    GroudUnderPlayer.Add(other.gameObject);
                }   
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        GroudUnderPlayer.Remove(other.gameObject);
    }
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S))
        {
           
        }

        if (IsJumping && _rigidbody.velocity.y <= 0)
        {
            IsJumping = false;
            IsFalling = true;
        }


        bool JumpCommand = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
        if ( JumpCommand && RemainJump > 0)
        {
            RemainJump -= 1;
            IsJumping = true;
            _rigidbody.velocity += Vector2.up * JumpForce;
        }

        if (IsJumping && Input.GetKey(KeyCode.Space))
        {
            _rigidbody.gravityScale = JumpingGratityScale;
        }
        else
        {
            _rigidbody.gravityScale = DefaultGratityScale;
        }

        HorizontalMovement = Input.GetAxis("Horizontal");
        
        bool RunningButton = Input.GetKey(KeyCode.LeftControl);


        Vector2 offset;
        if (RunningButton)
        {
            CurrentSpeed = RunningSpeed;
            offset = new Vector2(HorizontalMovement * RunningSpeed * Time.deltaTime,0);
        }
        else
        {
            CurrentSpeed = Speed;
            offset = new Vector2(HorizontalMovement * Speed * Time.deltaTime,0);
        }

        if (offset.x == 0)
        {
            IsWalking = false;
            IsRunning = false;
        }else if (RunningButton)
        {
            IsRunning = true;
            IsWalking = false;
        }
        else
        {
            IsRunning = false;
            IsWalking = true;
        }
        _rigidbody.position += offset;
        _rigidbody.velocity = new Vector2(
            Mathf.Clamp(_rigidbody.velocity.x, -60, 60),
            Mathf.Clamp(_rigidbody.velocity.y, -60, 50));
        
        // AnimationType thisFrameAnimation = GetThisFrameAnimation();
        // if (thisFrameAnimation != currentAnimation)
        // {
        //     currentAnimation = thisFrameAnimation;
        //     ChangingAnimationEvent?.Invoke(thisFrameAnimation,MovingDirection);
        // }
    }

    // private AnimationType GetThisFrameAnimation()
    // {
    //     
    // }

    // public enum AnimationType
    // {
    //     walk,
    //     idle,
    //     jump,
    //     falling,
    // }
    // public delegate void SampleEventHandler(AnimationType animationType,Vector2 direction);
    // public event SampleEventHandler ChangingAnimationEvent;

   /* public void Lama()
    {
        isLama = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsLama);

        if (isLama == true)
        {
            this.vel = 5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = UnityEngine.Vector2.up * (jumpForce / 2);
            }
        }
    }*/

    public void Poison()
    {
        isPoison = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsPoison);
        if (isPoison == true)
        {
            Destroy(gameObject);

        }
    }

    public void Trampolim()
    {
        isTrampolim = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsTrampolim);

        if (isTrampolim == true)
        {

            rb.velocity = UnityEngine.Vector2.up * (jumpForce); ;
        }
    }
}
