using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public GameObject player;           // ref to player
    public GameObject activateObject;   // store ref of object to activate
    public Camera objectCam;            // store ref to object camera view (if any)
    bool canPress = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && canPress)
            {
                StartCoroutine(Activate());
            }
        }
    }

    void PressButton()
    {
        // change button to green color to represent activation
        transform.gameObject.GetComponent<Renderer>().material.color = Color.green;

        canPress = false;   // can not press button while button activated
    }

    IEnumerator Activate()
    {
        PressButton();

        if (objectCam != null) // check if cam angle
        {
            objectCam.GetComponent<Camera>().enabled = true;    // enable cam view angle
        }

        // call object function to activate object behaviour
        activateObject.SendMessage("Activate");

        yield return new WaitForSeconds(3.5f);

        objectCam.GetComponent<Camera>().enabled = false;    // disable object cam -> return to player cam

        canPress = true;
    }
}
