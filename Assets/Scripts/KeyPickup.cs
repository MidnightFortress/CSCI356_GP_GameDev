using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    //variable to hold this key name
    string key;

    KeyInventory keyInventory;

    private void Start()
    {
        key = gameObject.name;
        keyInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && keyInventory.GetKey().Equals("None"))
        {
            keyInventory.SetKey(key);

            Debug.Log("You've got the " + key);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("You already have a key!");
        }
    }

}