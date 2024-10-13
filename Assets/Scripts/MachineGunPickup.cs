using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// acts as a pick up item to engage machine gun script behaviors

public class MachineGunPickup : MonoBehaviour
{

    GameObject machineGun;
    GameObject weaponHolder;
    GunManager gunManager;

    private void OnTriggerEnter(Collider other)
    {
        // check player has collided
        if (other.transform.CompareTag("Player"))
        {

            gunManager = other.GetComponentInChildren<GunManager>();

            if (gunManager != null)
            {

                // disable other RH weapon scripts on child camera
                other.transform.GetChild(0).GetComponent<HandGun>().enabled = false;
                weaponHolder.transform.GetChild(0).gameObject.SetActive(false);

                // enable machine gun script behaviour
                other.transform.GetChild(0).GetComponent<MachineGun>().enabled = true;
                // enable maching gun game object
                weaponHolder.transform.GetChild(1).gameObject.SetActive(true);

                gunManager.AddWeapon(weaponHolder.transform.GetChild(1).gameObject);
                // passing teh object to the gun manager

                // start the pick up respawn coroutine
                StartCoroutine(RespawnPickup(machineGun));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get machine gun game object reference
        machineGun = transform.GetChild(0).gameObject;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }

    // Update is called once per frame
    void Update()
    {
        // spin the machine gun game object via the parent transform
        transform.Rotate(0.0f, 50.0f * Time.deltaTime, 0.0f, Space.Self);
    }

    public IEnumerator RespawnPickup(GameObject machineGun)
    {
        machineGun.SetActive(false);    // disable machine gun game object

        yield return new WaitForSeconds(5);

        machineGun.SetActive(true);
    }
}
