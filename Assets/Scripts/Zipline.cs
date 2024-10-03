using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    [SerializeField] Zipline targetZip;
    [SerializeField] float zipSpeed = 400f;
    [SerializeField] float zipScale = 0.2f;
    [SerializeField] float arrivalThreshold = 0.5f;

    public Transform zipTransform;

    bool zipping = false;
    GameObject zipPulley;

    // Update is called once per frame
    void Update()
    {
        // make sure zipping before running update code
        if (!zipping || zipPulley == null)
            return;

        zipPulley.GetComponent<Rigidbody>().AddForce((targetZip.zipTransform.position - zipTransform.position).normalized * zipSpeed * Time.deltaTime, ForceMode.Acceleration);

        if (Vector3.Distance(zipPulley.transform.position, targetZip.zipTransform.position) <= arrivalThreshold)
        {
            ResetZipline();
        }
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
        player.transform.position += Vector3.up * 0.5f;         // little move up to simulate grabbing on to pulley

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
        GameObject player = zipPulley.transform.GetChild(0).gameObject;

        // reset player to finish using zipline
        player.GetComponent<FPSInput>().enabled = true;                 // enable player control script                                                                
        player.transform.SetParent(null);                               // detach player form pulley

        Destroy(zipPulley);  // destroy zip pulley object
        zipPulley = null;    // set variable to null
        zipping = false;    // zipping finished, set variable to false
    }
}
