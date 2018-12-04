using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private Animator anim;

    private bool isPlayerInTrigger;

    private bool openDoor = false;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            openDoor = player.GiveDoorKey();
            spriteRenderer.enabled = true;
            anim.SetBool("hasKey", openDoor);


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.enabled = false;
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Activate") && isPlayerInTrigger && openDoor)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
