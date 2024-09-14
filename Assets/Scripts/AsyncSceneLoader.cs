using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{

    private AsyncOperation asyncOperation;
    public string sceneToLoad;
    private bool isSceneReady = false;

    private void OnTriggerEnter(Collider other)
    { // Unpause the game if it's paused before loading the next scene

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
      
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(LoadSceneAsyncCor());
        }
    }

    private IEnumerator LoadSceneAsyncCor()
    {
      
        asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
           asyncOperation.allowSceneActivation = false;

            Debug.Log(asyncOperation.progress);

        while (asyncOperation.progress < 0.9f)
            yield return null;
       
        Debug.Log("Scene is ready. Press the button to load it.");


    }


    public void ActivateScene()
    {

        asyncOperation.allowSceneActivation = true;
    }
      
}
