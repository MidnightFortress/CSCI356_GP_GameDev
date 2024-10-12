using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    GameObject grenade;

    private void OnTriggerEnter(Collider other)
    {
        // check player has collided
        if (other.transform.CompareTag("Player"))
        {
            // disable other weapon scripts on child camera
            other.transform.GetChild(0).GetComponent<HandGun>().enabled = false;
            other.transform.GetChild(0).GetComponent<MachineGun>().enabled = false;

            // enable grenade script behaviour
            other.transform.GetChild(0).GetComponent<Grenade>().enabled = true;

            // start respawn coroutine
            StartCoroutine(RespawnPickup(grenade));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get grenade game object reference
        grenade = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // spin/rotate the grenade game onject via the parent transform
        transform.Rotate(0.0f, 50.0f * Time.deltaTime, 0.0f, Space.Self);
    }

    public IEnumerator RespawnPickup(GameObject grenade)
    {
        // disable game object -> use cooroutine to wait and then respawn pick up
        grenade.SetActive(false);

        yield return new WaitForSeconds(5);     // 5 second respawn time

        grenade.SetActive(true);
    }
}
