using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotControllerScript : MonoBehaviour {

    //variables that need to be set within Unity
    [SerializeField]
    private float maxSpeed, jumpForce, gravityForce;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    private Collider2D playerGroundCollider;

    //variables that dont need to be set within Unity
    private float move;
    private bool facingRight;
    private bool grounded;
    private bool died;
    private float groundRadius;
    private Checkpoint currentCheckpoint;
    private int beforeRespawn;
    private int timer;
    private AudioSource audioSource;
    private bool pickUpKey;
    private Rigidbody2D rb2d;
    private Animator anim;

    //starts as soon as the game lauches, gives the code access to the audio source on the player
    private void Start()
    {
        facingRight = true;
        grounded = false;
        died = false;
        groundRadius = 0.2f;
        beforeRespawn = 5000;
        timer = 0;
        pickUpKey = false;

        audioSource = GetComponent<AudioSource>();
        playerGroundCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    //updates the players physics material between stop and moving and also updates the player actually moving

    //updates to check whether the player is on the ground on their jumping
    private void Update()
    {
        UpdateIsGrounded();
        Jump();
    }

    //runs how the player will move
    private void Move()
    {
        //as long as the player hasn't died and needs to respawn the moving code will work (so they cant move around while playing the death animation)
        if(anim.GetBool("Death") == false)
        {

            //checks if the player is entering any kind of move input on the controller
            move = Input.GetAxisRaw("Horizontal");

            //adjusts the animation of the character
            anim.SetFloat("Speed", Mathf.Abs(move));

            //moves the character and flips them depending on which way they are facing
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

    //flips the character depending on which way they are facing
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //checks if the player is touching the ground or not
    private void UpdateIsGrounded()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //updates the animator variables on whether the player is on the ground or not
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rb2d.velocity.y);
    }

    //works the characters jumping
    private void Jump()
    {

        //runs the code as long as they havnt died (so they cant jump while playing the death animation)
        if (anim.GetBool("Death") == false)
        {
            //checks if the player is on the ground and pressing the jump button
            if (grounded && Input.GetButtonDown("Jump"))
            {
                //makes the player jump into the air and updates the animators variable for being on the ground or not
                anim.SetBool("Ground", false);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        
    }

    //updates the players physics materials
    private void UpdatePhysicsMaterial()
    {
        //if the player is moving then they have the moving material set
        if(Mathf.Abs(move) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        //if the player isnt moving then the player has the stopping material set
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    //respawns the charater if they have died
    private void Respawn()
    {
        //if the player doesnt have a set checkpoint and dies then the game reloads the level
        if (currentCheckpoint == null)
        {
            //updates the animators variable for the players death back to false so they arnt stuck in the death animation
            anim.SetBool("Death", false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //resets the gravity scale
            rb2d.gravityScale = gravityForce;

        }

        //if the player does have a checkpoint then they respawn there instead
        else
        {
            rb2d.velocity = Vector2.zero;
            anim.SetBool("Death", false);
            transform.position = currentCheckpoint.transform.position;
            rb2d.gravityScale = gravityForce;
        }
    }

    //sets the players current checkpoint (from the checkpoint script)
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        //if the player has a checkpoint already it will be turned off
        if (currentCheckpoint != null)
        {
            currentCheckpoint.SetIsActivated(false);
        }

        //the current checkpoint will be turned on
        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }

    //plays the players death (from the hazards script)
    public void Death()
    {
        //plays the death noise, turns the gravity to 0 so they stay still, turns off all forces on the player, and updates the animators death variable
        audioSource.Play();
        anim.SetBool("Death", true);
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector2.zero;

        //runs the respawn code after a second of playing the death animation
        Invoke("Respawn", 1);
    }

    //checks if the player has the key to the door (from the keys script)
    public void DoesPlayerHaveKey(bool answer)
    {
        anim.SetBool("hasKey", answer);
        pickUpKey = answer;
    }

    //gives the door the key (from the door script)
    public bool GiveDoorKey()
    {
        return pickUpKey;
    }
}
