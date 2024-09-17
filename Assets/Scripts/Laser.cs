using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Vector3 direction;
    private float speed;

    void Start(){
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the laser in the set direction at the given speed
        transform.position += direction * speed * Time.deltaTime;
    }

    public void setDirection(Vector3 shootDirection, float laserSpeed)
    {
        direction = shootDirection.normalized;
        speed = laserSpeed;

        transform.forward = direction;
        transform.Rotate(90f, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!((collision.gameObject.tag == "Player") | (collision.gameObject.tag == "Monster")))
        {
            Destroy(gameObject);
        }

    }
}
