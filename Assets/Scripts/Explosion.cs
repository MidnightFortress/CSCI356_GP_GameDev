using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// force rigidbody dependency
[RequireComponent(typeof(Rigidbody))]

public class Explosion : MonoBehaviour
{
    // radius and force variables
    public float blastRadius = 6.0f;
    public float blastPower = 5.0f;
    public float upForce = 1.0f;

    public int grenDamage = 100;
    private Droids droid;

    // process collision event
    private void OnCollisionEnter(Collision collision)
    {
        // obtain postion of the explosion
        Vector3 blastPos = transform.position;

        // generate collsion sphere to affect objects within blastRadius, store colliders in array
        Collider[] colliders = Physics.OverlapSphere(blastPos, blastRadius);

        // process all objects found to be in the collision sphere/blastRadius (in colliders array) from explosion position
        foreach (Collider hit in colliders)
        {
            // obtain rigidbody of hit object
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();
    
            // check hit object contains a rigidbody component
            if (rigidbody != null)
            {
                // turn on gravity of rigidbody
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;

                // apply impulse force based on blastPower
                rigidbody.AddExplosionForce(blastPower, blastPos, blastRadius, upForce, ForceMode.Impulse);
            }

            // get the Droids component and call take damage
            if(hit.gameObject.TryGetComponent<Droids>(out droid))
            {
                droid.TakeDamage(grenDamage, false);
            };
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
