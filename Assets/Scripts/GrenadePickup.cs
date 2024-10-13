using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrenadePickup : MonoBehaviour
{
    GameObject grenade;
    GameObject weaponHolder;
    //private SceneManager sceneManager;
    //public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        // check player has collided
        if (other.transform.CompareTag("Player"))
        {
            //SceneManager.LoadScene(sceneToLoad);

            // enable grenade script behaviour
            other.transform.GetChild(0).GetComponent<Grenade>().enabled = true;
            weaponHolder.transform.GetChild(2).gameObject.SetActive(true);

            // start respawn coroutine
            StartCoroutine(RespawnPickup(grenade));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get grenade game object reference
        grenade = transform.GetChild(0).gameObject;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
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
