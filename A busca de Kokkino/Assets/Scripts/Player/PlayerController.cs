using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{


    float vel = 10f;
    public Rigidbody2D rb;
    public Transform feetPos;

    public float checkRadius;

    public LayerMask whatIsGround;
    public LayerMask whatIsLama;
    public LayerMask whatIsPoison;
    public LayerMask whatIsTrampolim;

    public float jumpForce;

    private bool isLama;
    private bool isGrounded;
    private bool isPoison;
    private bool isTrampolim;

    private void cc()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }
    void Update()
    {
        Mover();
        Pular();
        Lama();
        Poison();
        cc();
        Trampolim();
    }
   
    private void Mover() 
    {
        if (isGrounded == true)
        {
            vel = 10f;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * vel;
        rb.velocity = new UnityEngine.Vector2(moveBy, rb.velocity.y);
    } 

    private void Pular()
    {
 
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = UnityEngine.Vector2.up * jumpForce;
        }
    }

    public void Lama()
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
    }

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