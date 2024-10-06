using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Vector3 direction;
    private float speed;

    private float damage;

    void Start(){
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the laser in the set direction at the given speed
        transform.position += direction * speed * Time.deltaTime;
    }

    public void setDirection(Vector3 shootDirection, float laserSpeed, float laserDamage)
    {
        direction = shootDirection.normalized;
        speed = laserSpeed;

        transform.forward = direction;
        transform.Rotate(90f, 0, 0);
        damage = laserDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<FPSInput>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "droid" | collision.gameObject.tag == "droidCrit")
        {
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
