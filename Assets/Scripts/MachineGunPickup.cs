using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// acts as a pick up item to engage machine gun script behaviors

public class MachineGunPickup : MonoBehaviour
{

    GameObject machineGun;

    private void OnTriggerEnter(Collider other)
    {
        // check player has collided
        if (other.transform.CompareTag("Player"))
        {
            // disable other weapon scripts on child camera
            other.transform.GetChild(0).GetComponent<HandGun>().enabled = false;
            other.transform.GetChild(0).GetComponent<Grenade>().enabled = false;

            // enable machine gun script behaviour
            other.transform.GetChild(0).GetComponent<MachineGun>().enabled = true;


            // start the pick up respawn coroutine
            StartCoroutine(RespawnPickup(machineGun));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get machine gun game object reference
        machineGun = transform.GetChild(0).gameObject;
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
