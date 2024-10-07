using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertKey : MonoBehaviour, Interactable
{
    [SerializeField] private DoorTrigger door;
    private KeyInventory keyInventory;
    public GameObject requiredKey;
    public GameObject keyLock;

    private string key;

    private void Awake()
    {
        keyInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>();    // get ref to key inventory
        key = requiredKey.name;
    }

    public void Interact()
    {
        if (keyInventory.HasKey(key)) // if player has key
        {
            int keyIndex = keyInventory.GetKeyIndex(key);

            Vector3 pos = keyLock.transform.position;       // get position of key lock
            Quaternion rot = keyLock.transform.rotation;    // get rotation of key lock

            // insert the key
            Instantiate(requiredKey, pos, rot);             // create key prefab into key lock

            door.UseKey(requiredKey);                              // uses key

            door.Interact();                                // call interact to open door
            keyInventory.SetKeyAtIndex(keyIndex, "None");   // set key inventory position to noone
            gameObject.SetActive(false);
        }
    }
}
