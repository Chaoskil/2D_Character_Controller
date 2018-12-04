using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour {


    [SerializeField]
    private Animator anim;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private static int coinCount;



    // Use this for initialization
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Collected", true);
            boxCollider2D.enabled = false;
            coinCount++;
            Debug.Log("Coin count: " + coinCount);
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }	
	}
}
