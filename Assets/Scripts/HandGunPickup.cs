using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// acts as a pick up item to engage handgun script behaviors

public class HandGunPickup : MonoBehaviour
{
    GameObject handGun;
    GameObject weaponHolder;
    GunManager gunManager;

    private void OnTriggerEnter(Collider other)
    {
        // check player has collided
        /*   if (other.transform.CompareTag("Player"))
           {
               gunManager = other.GetComponentInChildren<GunManager>();

               // disable other RH weapon scripts on child camera
               other.transform.GetChild(0).GetComponent<MachineGun>().enabled = false;
               weaponHolder.transform.GetChild(1).gameObject.SetActive(false);

               if (gunManager != null)
               {
               // enable handgun script
               other.transform.GetChild(0).GetComponent<HandGun>().enabled = true;

               // enable handgun game object
               weaponHolder.transform.GetChild(0).gameObject.SetActive(true);

                   // also need to add to inventory
                   gunManager.AddWeapon(weaponHolder.transform.GetChild(0).gameObject);

                   // start respawn coroutine
                   StartCoroutine(RespawnPickup(handGun));
               }

           }*/

        if (other.transform.CompareTag("Player"))
        {
            gunManager = other.GetComponentInChildren<GunManager>();

            if (gunManager != null)
            {
                // Disable other RH weapon scripts on child camera
                other.transform.GetChild(0).GetComponent<MachineGun>().enabled = false;
                weaponHolder.transform.GetChild(1).gameObject.SetActive(false);

                // enable the currernt weapon
                other.transform.GetChild(0).GetComponent<HandGun>().enabled = true;

                gunManager.AddWeapon(weaponHolder.transform.GetChild(0).gameObject);
              // add the gun to the wepon manager

                // Start respawn coroutine
                StartCoroutine(RespawnPickup(handGun));
            } else
            {
                Debug.Log("No gunmanger");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get the hand gun game object reference
        handGun = transform.GetChild(0).gameObject;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }

    // Update is called once per frame
    void Update()
    {
        // spin/rotate the hand gun game object via the parent transform
        transform.Rotate(0.0f, 50.0f * Time.deltaTime, 0.0f, Space.Self);
    }

    // corooutine for respawing hand gun pick up
    public IEnumerator RespawnPickup(GameObject handGun)
    {
        handGun.SetActive(false);   // disable game object

        yield return new WaitForSeconds(5);

        handGun.SetActive(true);    // enable hand gun game object
    }
}
