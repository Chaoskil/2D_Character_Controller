using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    //variables that need to be set within Unity
    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private Animator anim;

    //variables that do not need to be set within Unity
    private bool isPlayerInTrigger;
    private bool openDoor = false;
    private SpriteRenderer spriteRenderer;

    //starts as soon as the game launches, gives the code access to the sprite renderer and turns it off immediatetly
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    //if the player walks into the trigger for the door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();

            //determines whether the player has the key for the door or not
            openDoor = player.GiveDoorKey();

            //turns the sprite renderer on so that the player can see if they can enter the door
            spriteRenderer.enabled = true;

            //tells the doors animator about the players key status
            anim.SetBool("hasKey", openDoor);
        }
    }

    //if the player walks out of the doors trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //turns off the sprite renderer
            spriteRenderer.enabled = false;
            isPlayerInTrigger = false;
        }
    }

    //if the player presses the enter button while inside the doors trigger and they have the key then they finish the level and move on to the next
    private void Update()
    {
        if (Input.GetButtonDown("Activate") && isPlayerInTrigger && openDoor)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
