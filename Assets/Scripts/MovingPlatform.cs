using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // platform path start and end points
    public List<Transform> wayPoints;

    //public Transform pointA;
    //public Transform pointB;

    Transform currentPoint;
    Transform nextPoint;
    bool movingForward = true;
    int currentPointIndex = 0;

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
        currentPoint = wayPoints[0];
        nextPoint = wayPoints[1];

        // calculate distance to next point
        totalDistance = Vector3.Distance(currentPoint.position, nextPoint.position);
    }

    // Update is called once per frame
    void FixedUpdate() // using fixed update gives smooth motion independent of frame rate
    {
        // distance = speed * time
        float currDistance = platformSpeed * (Time.time - startTime);

        // obtain the percentage of path travelled (current distance / total distance)
        float percentTravelled = currDistance / totalDistance;

        // add ease in and ease out of platform motion
        percentTravelled = Mathf.SmoothStep(0, 1, percentTravelled);

        // use the above to interpolate between points
        transform.position = Vector3.Lerp(currentPoint.position, nextPoint.position, percentTravelled);

        // include platform rotation based on point transform rotation
        transform.rotation = Quaternion.Lerp(currentPoint.rotation, nextPoint.rotation, percentTravelled);

        if (movingForward && percentTravelled >= 1) // have reached the next point (get next way point if any)
        {
            currentPointIndex++;    // make next way point current

            if (currentPointIndex == wayPoints.Count - 1) // check if there is a next waypoint
            {
                movingForward = false;      // head in opposite direction

                currentPoint = wayPoints[currentPointIndex];
                nextPoint = wayPoints[currentPointIndex - 1];

                totalDistance = Vector3.Distance(currentPoint.position, nextPoint.position);
                startTime = Time.time;
            }
            else
            {
                currentPoint = wayPoints[currentPointIndex];
                nextPoint = wayPoints[currentPointIndex + 1];

                totalDistance = Vector3.Distance(currentPoint.position, nextPoint.position);
                startTime = Time.time;
            }
        }
        else if (!movingForward && percentTravelled >= 1) // have reached the next point (get next way point if any)
        {
            currentPointIndex--;    // make next way point current

            if (currentPointIndex == 0) // check if there is a next waypoint
            {
                movingForward = true;      // head in opposite direction

                currentPoint = wayPoints[currentPointIndex];
                nextPoint = wayPoints[currentPointIndex + 1];

                totalDistance = Vector3.Distance(currentPoint.position, nextPoint.position);
                startTime = Time.time;
            }
            else
            {
                currentPoint = wayPoints[currentPointIndex];
                nextPoint = wayPoints[currentPointIndex - 1];

                totalDistance = Vector3.Distance(currentPoint.position, nextPoint.position);
                startTime = Time.time;
            }
        }

        // if next point reached alternate the platorm direction
        //if (percentTravelled >= 1)
        //{
        //    // swap transforms
        //    Transform temp = pointB;
        //    pointB = pointA;
        //    pointA = temp;

        //    // reset start time to current time
        //    startTime = Time.time;
        //}
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
        DontDestroyOnLoad(other);
    }

}
