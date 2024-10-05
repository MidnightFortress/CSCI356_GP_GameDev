using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, Interactable
{
    [SerializeField] GameObject[] targets;
    [SerializeField] List<GameObject> requiredKeys;
    public bool locked;
    private int numUsedKeys = 0;
 
    void OnTriggerEnter(Collider other)
    {  
        foreach (GameObject target in targets)
        {
                target.SendMessage("Activate");
        }

        // a bool taken from keymanger to only open the door associated with the key held
    }
    void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
        Debug.Log("Closing");
    }

    public void UseKey(GameObject key)    // only used if keys required
    {
        if (requiredKeys.Contains(key))
        {
            //numUsedKeys++;
            requiredKeys.Remove(key);
        }
        else
        {
            Debug.Log("Wrong key!");
        }
        

        // if all keys used unlock door
        if (locked && requiredKeys.Count == 0)
        {
            locked = false;
        }
    }

    // closing the doors when leaving the trigger

    public void Interact()      // uses raycast and checks for key
    {
        if(!locked)
        {
            foreach (GameObject target in targets)
            {

                target.SendMessage("Activate");
            }
        }
        else
        {
            Debug.Log("Door is locked.");
        }
    }
}

