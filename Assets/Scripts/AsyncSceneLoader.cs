using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{

    private AsyncOperation asyncOperation;
    public string sceneToLoad;
    private bool alreadyLoaded = false;

    private void OnTriggerEnter(Collider other)
    { 

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }


        if (other.CompareTag("Player")) 
        {
            if (!alreadyLoaded)
            {
            alreadyLoaded = true;  
            StartCoroutine(LoadSceneAsyncCor());
            }
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
