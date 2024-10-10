using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMan : MonoBehaviour
{

    [SerializeField] private Material skybox;
    private float elapsedTime = 0f;
    private float rotationSpeed = 15f;
    private static int Rotation = Shader.PropertyToID("_Rotation");
    // giving some roation to make the sky move

    void Start()
    {
        if (skybox != null)
        {
            RenderSettings.skybox = skybox;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        RenderSettings.skybox.SetFloat(Rotation, elapsedTime * rotationSpeed);
        // making the sky rotate
    }
    // this will create a single skybox, which will be the night Sky with moon
    // i followed a tutorial to get the skybox in, it can be found here https://www.youtube.com/watch?v=sye7KArFoTY
}

