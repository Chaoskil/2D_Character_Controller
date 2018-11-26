using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    [SerializeField]
    private string sceneToLoad;

    private bool isPlayerInTrigger;

    private bool openDoor = false;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            openDoor = player.GiveDoorKey();
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
