using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D playerGroundCollider;


    private float move;
    private bool facingRight = true;
    private bool grounded = false;
    private bool died = false;
    private float groundRadius = 0.2f;
    private Checkpoint currentCheckpoint;
    private int beforeRespawn = 5000;
    private int timer = 0;
    private AudioSource audioSource;
    private bool pickUpKey = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

        private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();
    }
    private void Update()
    {
        UpdateIsGrounded();
        Jump();
    }

    private void Move()
    {
        if(anim.GetBool("Death") == false)
        {
            move = Input.GetAxisRaw("Horizontal");
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
        if (anim.GetBool("Death") == false)
        {
            if (grounded && Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Ground", false);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        
    }
    private void UpdatePhysicsMaterial()
    {
        if(Mathf.Abs(move) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.SetIsActivated(false);
        }
        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
    private void Respawn()
    {
        if (currentCheckpoint == null)
        {
            anim.SetBool("Death", false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            rb2d.gravityScale = 7;

        }
        else
        {
            rb2d.velocity = Vector2.zero;
            anim.SetBool("Death", false);
            transform.position = currentCheckpoint.transform.position;
            rb2d.gravityScale = 7;
        }
    }
    public void Death()
    {
        audioSource.Play();
        anim.SetBool("Death", true);
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector2.zero;
        Invoke("Respawn", 1);
    }
    public void DoesPlayerHaveKey(bool answer)
    {
        anim.SetBool("hasKey", answer);
        pickUpKey = answer;
    }
    public bool GiveDoorKey()
    {
        return pickUpKey;
    }
}
