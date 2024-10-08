using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : MonoBehaviour, Interactable
{
    public Transform pointA;
    public Transform pointB;

    private bool moving = false;

    // movement start time
    private float startTime;

    // platform movement speed
    public float platformSpeed = 3.0f;

    // distance between points
    private float totalDistance;

    // Start is called before the first frame update
    void Start()
    {
        // get start time
        startTime = Time.time;

        // calculate distance to next point
        totalDistance = Vector3.Distance(pointA.position, pointB.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!moving)
            return;

        // distance = speed * time
        float currDistance = platformSpeed * (Time.time - startTime);

        // obtain the percentage of path travelled (current distance / total distance)
        float percentTravelled = currDistance / totalDistance;

        // add ease in and ease out of platform motion
        percentTravelled = Mathf.SmoothStep(0, 1, percentTravelled);

        // use the above to interpolate between points
        transform.position = Vector3.Lerp(pointA.position, pointB.position, percentTravelled);

        // include platform rotation based on point transform rotation
        transform.rotation = Quaternion.Lerp(pointA.rotation, pointB.rotation, percentTravelled);

        if (moving && percentTravelled >= 1) // have reached the next point (get next way point if any)
        {
            moving = false;    // stop moving

            // switch the points
            (pointA, pointB) = (pointB, pointA);
        }
    }

    public void Interact()
    {
        // reset start time
        startTime = Time.time;

        //start elevator
        moving = true;
    }

    // parent player to platform while in contact
    private void OnTriggerEnter(Collider other)
    {
        // set platorm as parent of colliding object
        other.transform.SetParent(transform);
    }

    // remove parenting when player no longer in contact with platform
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
