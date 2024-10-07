using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookShotUI : MonoBehaviour
{
    public bool hookText; // to indicate if the text is active
    private GameObject hookshotText; //the game object being activated/deactivated

    // Start is called before the first frame update
    void Start()
    {
        hookshotText = GameObject.Find("HookshotText");
        hookshotText.SetActive(false);
    }

    private void OnGUI() // making changes to the UI element
    {
        hookshotText.SetActive(hookText);
    }

    public void activateHookText()
    {
        hookText = true;
    }

    public void deactivateHookText()
    {
        hookText = false;
    }
}
