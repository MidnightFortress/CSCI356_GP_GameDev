using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertKey : MonoBehaviour, Interactable
{
    private KeyInventory keyInventory;
    public GameObject requiredKey;
    public GameObject keyLock;

    private string key;
    private DoorTrigger door;

    private void Awake()
    {
        door = GetComponent<DoorTrigger>();  // ref to Door Trigger script
        keyInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>();    // get ref to key inventory
        key = requiredKey.name;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) && keyInventory.HasKey(key)) // if player has key
        //{
        //    int keyIndex = keyInventory.GetKeyIndex(key);

        //    RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, castOffset, 0), castRadius, Vector3.forward);

        //    // check all ray cast hits 
        //    foreach (RaycastHit hit in hits)
        //    {
        //        if (hit.collider.CompareTag("KeyHole")) // look for keyhole tag
        //        {
        //            Vector3 pos = keyLock.transform.position;
        //            Quaternion rot = keyLock.transform.rotation;

        //            // if keyhole found insert the key
        //            Debug.Log("Inserting the " + key);

        //            Instantiate(requiredKey, pos, rot);                // create key prefab
        //            hit.collider.gameObject.GetComponent<DoorTrigger>().Interact();
        //            keyInventory.SetKeyAtIndex(keyIndex, "None");
        //        }
        //    }
        //}
    }

    public void Interact()
    {
        if (keyInventory.HasKey(key)) // if player has key
        {
            Debug.Log("player interacts");
            int keyIndex = keyInventory.GetKeyIndex(key);

            Vector3 pos = keyLock.transform.position;       // get position of key lock
            Quaternion rot = keyLock.transform.rotation;    // get rotation of key lock

            // insert the key
            Debug.Log("Inserting the " + key);

            Instantiate(requiredKey, pos, rot);             // create key prefab into key lock
            door.UnlockDoor();                              // set door to unlocked
            door.Interact();                                // call interact to open door
            keyInventory.SetKeyAtIndex(keyIndex, "None");   // set key inventory position to noone
        }
    }
}
