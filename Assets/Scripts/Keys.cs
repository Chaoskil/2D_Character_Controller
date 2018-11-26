using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour {

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private float inactiveRotationSpeed = 100;

    [SerializeField]
    private float inactivatedScale = 1;

    [SerializeField]
    private Color inactivatedColor;


    // Use this for initialization
    private void Start () {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
        { 
        if (collision.gameObject.CompareTag("Player"))
        {
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            player.DoesPlayerHaveKey(true);
            boxCollider2D.enabled = false;
            audioSource.Play();
            Death();
        }
    }

    private void Death()
    {
        spriteRenderer.enabled = false;
        Destroy(gameObject);
    }



    private void Update()
    {
        UpdateRotation();
        UpdateScale();
    }

    private void UpdateColor()
    {
        Color color = inactivatedColor;

        spriteRenderer.color = color;
    }
    private void UpdateScale()
    {
        float scale = inactivatedScale;

        transform.localScale = Vector3.one * scale;
    }
    private void UpdateRotation()
    {
        float rotationSpeed = inactiveRotationSpeed;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

}
