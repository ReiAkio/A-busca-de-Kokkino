using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRapha : MonoBehaviour
{
    public GameObject playerObject;

    private Rigidbody2D rb;

    [Header("Player properites")]
    public float speed;
    public int maxJump;


    [Header("Controls")]
    public KeyCode jumpButton;
    public KeyCode rightButton;
    public KeyCode leftButton;
    public KeyCode upButton;
    public KeyCode downButton;

    

    void Start()
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void sideMovements()
    {
        if (Input.GetKey(rightButton))
        {
            rb.transform.position = new Vector2(rb.position.x + speed, rb.position.y);
        }
        if (Input.GetKey(rightButton))
        {
            rb.transform.position = new Vector2(rb.position.x - speed, rb.position.y);
        }
        else
            rb.transform.position = new Vector2(rb.position.x, rb.position.y);
    }

}
