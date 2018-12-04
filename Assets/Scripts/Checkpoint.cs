using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {


    //variable that needs to be applied within unity
    [SerializeField]
    private float inactivatedScale = 1, activatedScale = 1.5f;


    //other variables that dont need to be applied in Unity
    private bool isActivated = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;


    //works as soon as the game starts, gives the code access to change srite renderer and audio source
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    //updates the checkpoints scale regulary
    private void Update()
    {
        UpdateScale();
    }

    //changes the scale of the checkpoint depending on if it is the active checkpoint or not
    private void UpdateScale()
    {
        float scale = inactivatedScale;

        if (isActivated)
        {
            scale = activatedScale;
        }

        transform.localScale = Vector3.one * scale;
    }

    //checks if the player touches the checkpoint then makes it the current checkpoint they will respawn at
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            player.SetCurrentCheckpoint(this);
            audioSource.Play();
        }
    }

    //Changes the scale of the checkpoint to indicate that it is the active one
    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpdateScale();
    }
}
