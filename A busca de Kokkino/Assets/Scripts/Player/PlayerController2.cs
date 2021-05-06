using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController2 : MonoBehaviour
{
    //OBS: Edit do Rapha -> Fragmentar mais o código em funcões, muito procedural gera dificuldade do acoplamento com as outras partes do projeto

    private Rigidbody2D _rigidbody;
    public Transform feetPos;
    //private AnimationType currentAnimation;


    [Header("Custom Properites for PlayerController")]
    public float Angle = 135;
    public float jumpForce = 50;
    public float Speed = 20;
    public float RunningSpeed = 40;
    public int RemainJump = 0;
    public int MaxJump = 2;
    public float JumpingGratityScale = 8;
    public float DefaultGratityScale = 12;
    public float HorizontalMovement;
    public float CurrentSpeed;
    public float checkRadius;

    [Header("Player movement status")]
    public bool IsJumping;
    public bool IsFalling;
    public bool IsWalking;
    public bool IsRunning;

    [Header("Player Effects variables")] 
    public float MudSpeedMultiplier = 0.5f;
    public float TranpolineJumpPower = 5;
    [Header("Player Effects")] 
    public bool IsDead;

    [Header("Player ground interaction Status")]
    public LayerMask whatIsGround;
    public LayerMask whatIsLama;
    public LayerMask whatIsPoison;
    public LayerMask whatIsTrampolim;
    
    public bool IsOnMud;
    public bool IsOnTrampoline;
    public bool isOnGround;
    public bool isOnPoison;

    
    public bool IsGrounded
    {
        get { return GroudUnderPlayer.Count > 0; }
    }

    public bool IsIdle
    {
        get { return IsGrounded && !IsJumping && !IsFalling && !IsWalking && !IsRunning; }
    }
    
    public List<GameObject> GroudUnderPlayer = new List<GameObject>(); 
    public List<GameObject> TouchingThePlayer = new List<GameObject>();

    private void UpdatePlayer()
    {
        IsDead = false;
        IsOnMud = false;
        IsOnTrampoline = false;

        foreach (GameObject ObjectsTouchingPlayer in TouchingThePlayer) // Padrao de identificacao dos objetos --->> Layers e Tags
        {
            string name = ObjectsTouchingPlayer.name; //Alterar para comparar com as TAGS para o código ficar compatível com outras implementacões
            if (name == "Poison" )
            {
                IsDead = true;
            }

            if (name == "Lama")
            {
                IsOnMud = true;
                MudSpeedMultiplier = 0.5f;
            }
            else
            {
                MudSpeedMultiplier = 1f;
            }

            if (name == "Trampolim")
            {
                IsOnTrampoline = true;
                IsJumping = true;
                _rigidbody.velocity += Vector2.up * (jumpForce * MudSpeedMultiplier);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D[] contactPoints = other.contacts;
        bool isTheGround = true; //Pode colidir com objetos fora do chão ----> refatorar
        
        foreach (ContactPoint2D contactPoint in contactPoints) // -------------------------------- ESQUECE
        {
            Vector2 point = contactPoint.point;             // Ponto onde ocorre o contato
            Vector2 center = this.transform.position;       // Posicao do jogador

            var AB = point - center;                        

            if (!(Mathf.Cos(Angle / 2) < Vector2.Dot(AB, Vector2.down))) //Comentários?
            {
                Debug.DrawLine(center,point,Color.red,0.1f);
                isTheGround = false;
            }
            else
            {
                Debug.DrawLine(center,point,Color.green,0.1f);
            }
        }                                                      // --------------------------------- ESQUECE
        var otherGameObject = other.gameObject;
        TouchingThePlayer.Add(otherGameObject);         // Adiciona ao vetor de objetos tocando no jogador
        if (isTheGround)
        {
            IsFalling = false;
            RemainJump = MaxJump;
            GroudUnderPlayer.Add(otherGameObject);
            //return;
            // }   
        }

        UpdatePlayer();
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        TouchingThePlayer.Remove(other.gameObject);
        GroudUnderPlayer.Remove(other.gameObject);
        UpdatePlayer();                                 // talvez possa ser excluído
    }


    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {

        // if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.S))
        // {
        //    
        // }

        Lama();
        Trampolim();
        Ground();
        Move();
        Poison();
        // AnimationType thisFrameAnimation = GetThisFrameAnimation();
        // if (thisFrameAnimation != currentAnimation)
        // {
        //     currentAnimation = thisFrameAnimation;
        //     ChangingAnimationEvent?.Invoke(thisFrameAnimation,MovingDirection);
        // }
    }

    private void Move()
    {
        if (IsJumping && _rigidbody.velocity.y <= 0)        // Atualizacao de status
        {
            IsJumping = false;
            IsFalling = true;
        }


        bool jumpCommand = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);  // Teclas para pulo
        if (jumpCommand && RemainJump > 0)
        {
            RemainJump -= 1;
            IsJumping = true;
            _rigidbody.velocity += Vector2.up * (jumpForce * MudSpeedMultiplier);
        }

        if (IsJumping && Input.GetKey(KeyCode.Space)) //Segurando espaco --> pula mais alto / fica mais tempo no ar
        {
            _rigidbody.gravityScale = JumpingGratityScale;
        }
        else
        {
            _rigidbody.gravityScale = DefaultGratityScale; //Sem segurar espaco -> cai mais rápido
        }

        HorizontalMovement = Input.GetAxis("Horizontal");

        bool RunningButton = Input.GetKey(KeyCode.LeftControl);


        Vector2 offset;
        if (RunningButton)
        {
            CurrentSpeed = RunningSpeed;
            offset = new Vector2(HorizontalMovement * RunningSpeed * Time.deltaTime, 0);
        }
        else
        {
            CurrentSpeed = Speed;
            offset = new Vector2(HorizontalMovement * Speed * Time.deltaTime, 0);
        }

        if (offset.x == 0)          // ta feio
        {
            IsWalking = false;
            IsRunning = false;
        }
        else if (RunningButton)
        {
            IsRunning = true;
            IsWalking = false;
        }
        else
        {
            IsRunning = false;
            IsWalking = true;
        }
        _rigidbody.position += offset * MudSpeedMultiplier;
        _rigidbody.velocity = new Vector2(
            Mathf.Clamp(_rigidbody.velocity.x, -60, 60),
            Mathf.Clamp(_rigidbody.velocity.y, -60, 50));       // Não deixa velocidade ultrapassar o limite maximo e minimo
    }
    private void Ground()
    {
        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround); //Verifica se whatIsGround ocorre dentro do raio (checkradius) a partir do ponto feet.position
    }
    public void Lama()
    {
        IsOnMud = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsLama);

        if (IsOnMud == true)
        {
            this.Speed = 5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.velocity = UnityEngine.Vector2.up * (jumpForce / 2);
            }
        }
    }

    public void Poison()
    {
        isOnPoison = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsPoison);
        if (isOnPoison)
        {
            IsDead = true;

        }
        IsDead = false;
    }

    public void Trampolim()
    {
        IsOnTrampoline = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsTrampolim);

        if (IsOnTrampoline == true)
        {

            _rigidbody.velocity = UnityEngine.Vector2.up * (jumpForce); ;
        }
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

}


