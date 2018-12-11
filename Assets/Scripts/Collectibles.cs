using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour {
    
    //variables that dont need to be set in unity
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Animator anim;

    //a coin count for how many chests the player has collected throughout the game
    private static int coinCount;



    //starts as soon as the game launches, gives the code access to the audio source, sprite renderer, and box collider
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    //once the player touchers the collectible then the collectible is destroyed and the player score is increased
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //plays the animation for the collectible, turns off its hitbox, plays its sound, and increases player score
            anim.SetBool("Collected", true);
            boxCollider2D.enabled = false;
            coinCount++;
            audioSource.Play();

            //destorys the game object after everything is done
            Destroy(gameObject, audioSource.clip.length);
        }	
	}
}
