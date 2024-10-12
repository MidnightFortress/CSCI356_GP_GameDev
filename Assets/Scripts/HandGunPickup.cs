using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// acts as a pick up item to engage handgun script behaviors

public class HandGunPickup : MonoBehaviour
{
    GameObject handGun;

    private void OnTriggerEnter(Collider other)
    {
        // check player has collided
        if (other.transform.CompareTag("Player"))
        {
            // disable other weapon scripts on child camera
            //other.transform.GetChild(0).GetComponent<MachineGun>().enabled = false;
            //other.transform.GetChild(0).GetComponent<Grenade>().enabled = false;

            // enable handgun script
            other.transform.GetChild(0).GetComponent<HandGun>().enabled = true;

            // start respawn coroutine
            StartCoroutine(RespawnPickup(handGun));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get the hand gun game object reference
        handGun = transform.GetChild(0).gameObject;
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
