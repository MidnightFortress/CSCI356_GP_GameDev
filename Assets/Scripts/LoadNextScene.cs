using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour, Interactable
{
    public AsyncSceneLoader asyncSceneLoader;
    public void Interact()
    {
        Debug.Log("Button Works");


      /*  // Unpause the game if it's paused before loading the next scene
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }*/

        if (asyncSceneLoader != null)
        {
            asyncSceneLoader.ActivateScene();
        }
        else
        {
            Debug.LogError("AsyncSceneLoader is not assigned!");
        }
    } 
}
