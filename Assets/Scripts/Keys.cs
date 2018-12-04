using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour {

    //variables that need to be set within Unity
    [SerializeField]
    private float inactiveRotationSpeed = 100;

    [SerializeField]
    private float inactivatedScale = 1;

    [SerializeField]
    private Color inactivatedColor;

    //variables that dont need to be set in Unity
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    

    //works as soon as the game starts, allows the code to have access to the audio soucre, sprite renderer, and the box collider
    private void Start () {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //Once the player touches the key then they collect it similar to the collectibles
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Player"))
        {
            //gets access to the players script and tells it that the player has the key
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            player.DoesPlayerHaveKey(true);

            //turns off the keys hitbox and sends a message saying that the player has the key, plays the key sound as well
            boxCollider2D.enabled = false;
            Debug.Log("You have the key!");
            audioSource.Play();

            //destroys the key object in the level
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
