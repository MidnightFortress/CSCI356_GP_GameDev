using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float currentTime = 0.0f;
    private Text outText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0.0f;
        outText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        TimeSpan timeout = TimeSpan.FromSeconds(currentTime);
        outText.text = string.Format("{0:00}:{1:00}", timeout.Minutes, timeout.Seconds);

    }
}
