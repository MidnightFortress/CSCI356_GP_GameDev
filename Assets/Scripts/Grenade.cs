using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // store camera component
    private Camera cam;

    // store grenade prefab
    public GameObject grenadePrefab;

    // grenade throw force variables
    public float grenadeImpulse = 5.0f;     // default throw impulse
    private float chargedImpulse;           // holds charged throw impulse
    public float chargeRate = 0.2f;         // rate of throw impulse charge
    public float maxThrowForce = 15.0f;     // maximum throw impulse
    private float chargeTimer = 0;          // timer to implement impulse charging

    // Start is called before the first frame update
    void Start()
    {
        // get reference to camera
        cam = GetComponent<Camera>();
        chargedImpulse = grenadeImpulse; // set charged to default impulse
    }

    // Update is called once per frame
    void Update()
    {
        // charge grenade throw
        if (Input.GetMouseButton(0))
        {
            chargeTimer += Time.deltaTime;

            if (chargeTimer >=  chargeRate && chargedImpulse < maxThrowForce) // increase throw impulse force to max 15
            {
                chargedImpulse += 1.0f;
                chargeTimer = 0;
            }
        }

        // throw the grenade
        if (Input.GetMouseButtonUp(0))
        {
            // create grenade object
            GameObject grenade = Instantiate(grenadePrefab, transform);

            // set grenade forward 2 units
            grenade.transform.position = cam.transform.position + cam.transform.forward * 2;

            Rigidbody target = grenade.GetComponent<Rigidbody>();

            // calculate throwing impulse
            Vector3 impulse = cam.transform.forward * chargedImpulse;

            // apply throw impulse force to grenade object
            target.AddForceAtPosition(impulse, cam.transform.position, ForceMode.Impulse);

            grenade.transform.SetParent(null);  // release grenade from camera control once thrown

            chargedImpulse = grenadeImpulse;  // reset to default throw strength
        }
        
    }
}
