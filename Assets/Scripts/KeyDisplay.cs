using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyInventory))]
public class KeyDisplay : MonoBehaviour
{
    public bool redKey = false;
    public bool blueKey = false;

    private KeyInventory keys;
    private GameObject redIndicator;
    private GameObject blueIndicator;

    // Start is called before the first frame update
    void Start()
    {
        keys = gameObject.GetComponent<KeyInventory>();
        redIndicator = GameObject.Find("RedKeyIndicator");
        blueIndicator = GameObject.Find("BlueKeyIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (keys.HasKey("Red Key"))
        {
            redKey = true;
        }
        else
        {
            redKey = false;
        }

        if (keys.HasKey("Blue Key"))
        {
            blueKey = true;
        }
        else
        {
            blueKey = false;
        }
    }

    private void OnGUI()
    {
        if (redKey)
        {
            redIndicator.SetActive(true);
        }
        else
        {
            redIndicator.SetActive(false);
        }

        if (blueKey)
        {
            blueIndicator.SetActive(true);
        }
        else
        {
            blueIndicator.SetActive(false);
        }
    }
}
