using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMan : MonoBehaviour
{

    [SerializeField] Material[] skyboxs;
    private float elapsedTime = 0f;
    private float cycleDuration = 30f;
    private float transitionDuration = 5f;
    private int currrentSkyboxIndex = 0;
    private static int Rotation = Shader.PropertyToID("_Rotation");
    private float rotationSpeed = 3f;

    private Material currentSkybox;
    private Material nextSkybox;
    private float transitionProgress = 0f;

    void Start()
    {
        if (skyboxs.Length > 0) { 
            currentSkybox = new Material(skyboxs[currrentSkyboxIndex]);
            RenderSettings.skybox = skyboxs[currrentSkyboxIndex];
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        Debug.Log("Elapsed time: " + elapsedTime);
        Debug.Log("Current skybox index: " + currrentSkyboxIndex);

        RenderSettings.skybox.SetFloat(Rotation, elapsedTime * rotationSpeed);

        if (elapsedTime >= cycleDuration) {
            elapsedTime = 0;

            currrentSkyboxIndex = (currrentSkyboxIndex + 1) % skyboxs.Length;
            nextSkybox = new Material(skyboxs[currrentSkyboxIndex]);
            StartCoroutine(TransitionSkybox(nextSkybox)); 
     
        }
        Debug.Log("Switched to skybox: " + currrentSkyboxIndex);
    }

    IEnumerator TransitionSkybox(Material targetSkybox)
    {
        transitionProgress = 0f;
        
        Material originalSkybox = RenderSettings.skybox;

        while (transitionProgress < 1f)
        {
            
            transitionProgress += Time.deltaTime / transitionDuration;

           
            RenderSettings.skybox.Lerp(originalSkybox, targetSkybox, transitionProgress);

           
            yield return null;
        }

        RenderSettings.skybox = targetSkybox;
    }
}
   
