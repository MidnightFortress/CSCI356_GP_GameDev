using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Debug.Log("Button Works");


        // Unpause the game if it's paused before loading the next scene
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +1);

        
      
    } 
}
