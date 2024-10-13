using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // store camera component
    private Camera cam;
    private GameObject weaponHolder;
    //private GameObject player;

    // store grenade prefab
    public GameObject grenadePrefab;

    // grenade throw force variables
    public float grenadeImpulse = 5.0f;     // default throw impulse
    private float chargedImpulse;           // holds charged throw impulse
    public float chargeRate = 0.5f;         // rate of throw impulse charge
    public float maxThrowForce = 30.0f;     // maximum throw impulse
    private float chargeTimer = 0;          // timer to implement impulse charging
    private bool isThrowing = false;

    // Start is called before the first frame update
    void Start()
    {
        // get reference to camera
        cam = GetComponent<Camera>();
        chargedImpulse = grenadeImpulse; // set charged to default impulse
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // check if player dead -> stop all actions
        //if(player.GetComponent<Health>().IsPlayerDead())
        //{
        //    StopAllCoroutines();
        //    return;
        //}

        if (isThrowing)
            return;

        // charge grenade throw
        if (Input.GetKeyDown(KeyCode.G))
        {
            chargeTimer += Time.deltaTime;

            if (chargeTimer >=  chargeRate && chargedImpulse < maxThrowForce) // increase throw impulse force to max 15
            {
                chargedImpulse += 1.0f;
                chargeTimer = 0;
            }
        }

        // throw the grenade
        if (Input.GetKeyUp(KeyCode.G))
        {
            Transform grenadeTrans = weaponHolder.transform.GetChild(2).transform;
            weaponHolder.transform.GetChild(2).gameObject.SetActive(false);

            // create grenade object
            GameObject grenade = Instantiate(grenadePrefab, transform);

            // set grenade forward 2 units
            grenade.transform.position = grenadeTrans.position + grenadeTrans.forward * 2;

            Rigidbody target = grenade.GetComponent<Rigidbody>();

            // calculate throwing impulse
            Vector3 impulse = grenadeTrans.forward * chargedImpulse;

            // apply throw impulse force to grenade object
            target.AddForceAtPosition(impulse, grenadeTrans.position, ForceMode.Impulse);

            grenade.transform.SetParent(null);  // release grenade from camera control once thrown

            chargedImpulse = grenadeImpulse;  // reset to default throw strength

            StartCoroutine(ThrowGrenade());
        }
    }

    public IEnumerator ThrowGrenade()
    {
        isThrowing = true;

        yield return new WaitForSeconds(2);

        isThrowing = false;
        weaponHolder.transform.GetChild(2).gameObject.SetActive(true);
    }
}
