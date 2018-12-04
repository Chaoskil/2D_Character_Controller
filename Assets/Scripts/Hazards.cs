using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazards : MonoBehaviour {

    //if the player touches the hazard then they will die and have to respawn
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RobotControllerScript player = collision.GetComponent<RobotControllerScript>();
            player.Death();
        }
    }
}
