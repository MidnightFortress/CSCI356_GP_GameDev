using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{

    private AsyncOperation asyncOperation;
    public string sceneToLoad;
    private bool isSceneReady = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(LoadSceneAsync());
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        
        asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false; 

       
        while (asyncOperation.progress < 0.9f)
        {
            yield return null; 
        }

        
        isSceneReady = true;
        Debug.Log("Scene is ready. Press the button to load it.");
    }

    public void ActivateScene()
    {
        if (isSceneReady)
        {
            asyncOperation.allowSceneActivation = true;
        }
    }
}
