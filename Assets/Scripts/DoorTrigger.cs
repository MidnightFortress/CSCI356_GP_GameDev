using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour, Interactable
{
    [SerializeField] GameObject[] targets;
    [SerializeField] GameObject requiredKey;
 
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

    // closing the doors when leaving the trigger

    public void Interact()      // uses raycast and checks for key
    {
        string key = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>().GetKey();

        Debug.Log("Player has the " + key);

        if (requiredKey == null)
        {
            foreach (GameObject target in targets)
            {

                target.SendMessage("Activate");
            }
        }
        // check if player has key
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>().GetKey() == requiredKey.name)
        {
            foreach (GameObject target in targets)
            {

                target.SendMessage("Activate");
            }
        }
    }
}

