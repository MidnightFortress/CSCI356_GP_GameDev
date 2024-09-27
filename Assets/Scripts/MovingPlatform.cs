using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // platform path start and end points
    public Transform pointA;
    public Transform pointB;

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
    void FixedUpdate() // using fixed update gives smooth motion not dependent on frame rate
    {
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

        // if next point reached alternate the platorm direction
        if (percentTravelled >= 1)
        {
            // swap transforms
            Transform temp = pointB;
            pointB = pointA;
            pointA = temp;

            // reset start time to current time
            startTime = Time.time;
        }
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
