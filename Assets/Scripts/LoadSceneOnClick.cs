using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    // Use this for initialization
    public void LoadSceneOnIndex(int screenIndex)
    {
        SceneManager.LoadScene(screenIndex);
    }
}
