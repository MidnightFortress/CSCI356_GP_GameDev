using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public Transform charController;
    bool onLadder = false;
    public float climbSpeed = 3.0f;
    public FPSInput FPSInput;

    // Start is called before the first frame update
    void Start()
    {
        FPSInput = GetComponent<FPSInput>();
        onLadder = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            Debug.Log("Climb Ladder");

            // disable the FPSInput script control
            FPSInput.enabled = false;
            onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            Debug.Log("Get Off Ladder");

            // enable the FPSInput Script control
            FPSInput.enabled = true;
            onLadder = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // specify the replacement controls when on ladder
        if (onLadder && Input.GetAxis("Vertical") == 1)
        {
            charController.transform.position += (Vector3.up * climbSpeed) * Time.deltaTime;
        }

        if (onLadder && Input.GetAxis("Vertical") == -1)
        {
            charController.transform.position += (Vector3.down * climbSpeed) * Time.deltaTime;
        }
    }
}
