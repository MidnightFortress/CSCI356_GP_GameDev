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
