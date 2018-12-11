using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    //credits to the Unity 2D main menu tutorial for this code;

    // Use this for initialization
    public void LoadSceneOnIndex(int screenIndex)
    {
        SceneManager.LoadScene(screenIndex);
    }
}
