using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControllerScript : MonoBehaviour {

    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask whatIsGround;


    private bool facingRight = true;
    private bool grounded = false;
    private float groundRadius = 0.2f;

    void Start () {

	}
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        UpdateIsGrounded();
        Jump();
    }

    private void Move()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));

        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void UpdateIsGrounded()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rb2d.velocity.y);
    }
    private void Jump()
    {
        if(grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Ground", false);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }
}
