using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovement : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded = true;
    private bool lookingRight;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        this.lookingRight = true;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        this.FlipSprite(Horizontal);
        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.cyan);

        if (Physics2D.Raycast(transform.position, Vector3.down, 1f))
        {
            Grounded = true;
        }
        else { 
            Grounded = false;
        }

        // Salto
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))&& Grounded)
        {
            Jump();
        }

    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2 (Horizontal * Speed, Rigidbody2D.velocity.y );
    }

    void FlipSprite(float direction)
    {
        if (direction == 0)
            return;

        // Got to right
        if (direction > 0 && this.lookingRight == false)
        {
            this.lookingRight = true;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Got to left
        else if (direction < 0 && this.lookingRight == true)
        {
            this.lookingRight = false;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
