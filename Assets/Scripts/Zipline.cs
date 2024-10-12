using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour, Interactable
{
    [SerializeField] private Zipline targetZip;
    [SerializeField] private float zipSpeed = 400f;
    [SerializeField] private float zipScale = 0.2f;
    [SerializeField] private float arrivalThreshold = 0.8f;

    public Transform zipTransform;

    private bool zipping = false;
    private GameObject zipPulley;
    private GameObject player;
    private GameObject grappleRope;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");            // ref to player object
        grappleRope = GameObject.FindGameObjectWithTag("Grapple-rope"); // ref to grapple rope
    }

    // Update is called once per frame
    void Update()
    {
        // make sure zipping before running update code
        if (!zipping || zipPulley == null)
            return;

        zipPulley.GetComponent<Rigidbody>().AddForce((targetZip.zipTransform.position - zipTransform.position).normalized * Time.deltaTime * zipSpeed, ForceMode.Acceleration);

        if (Vector3.Distance(zipPulley.transform.position, targetZip.zipTransform.position) <= arrivalThreshold)
        {
            ResetZipline();
        }
    }

    // interact function triggers zipline use
    public void Interact()
    {
        StartZipline(player);
    }

    public void StartZipline(GameObject player)
    {
        // make sure not already zipping
        if (zipping)
            return;

        // setup pulley unit for zipline use
        zipPulley = GameObject.CreatePrimitive(PrimitiveType.Sphere);                // create the pulley
        zipPulley.transform.position = zipTransform.position;                        // set pulley position to start of zipline
        zipPulley.transform.localScale = new Vector3(zipScale, zipScale, zipScale);  // set scale/size of pulley
        zipPulley.AddComponent<Rigidbody>().useGravity = false;                      // add rigid body with no gravity
        zipPulley.GetComponent<Collider>().isTrigger = true;                         // set collider to trigger

        // disable player control while zipping
        player.GetComponent<FPSInput>().enabled = false;
        grappleRope.SetActive(false);
        player.transform.position += Vector3.up * 0.5f;         // little move upward to simulate grabbing on to zipline pulley

        // set zip pulley to be parent of player
        player.transform.SetParent(zipPulley.transform);

        zipping = true;
    }

    private void ResetZipline()
    {
        // make sure zipping before running code
        if (!zipping)
            return;

        // get player reference
        //GameObject player = zipPulley.transform.GetChild(0).gameObject;

        // reset player to finish using zipline
        player.transform.SetParent(null);                               // detach player form pulley
        player.GetComponent<FPSInput>().ResetVelocity();
        player.GetComponent<FPSInput>().enabled = true;                 // enable player control script
        grappleRope.SetActive(true);

        // add player back to child of do not destroy on load
        

        Destroy(zipPulley);  // destroy zip pulley object
        zipPulley = null;    // set variable to null
        zipping = false;    // zipping finished, set variable to false
    }
}
