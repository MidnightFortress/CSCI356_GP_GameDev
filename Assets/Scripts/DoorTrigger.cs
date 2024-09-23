using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] targets;
 
    void OnTriggerEnter(Collider other)
    {
        
  foreach (GameObject target in targets)
        {
           
                target.SendMessage("Activate");
        }
       
      
        // a bool taken from keymanger to only open the door associated with the key held

        Debug.Log("Triger");
    }
    void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
        Debug.Log("Closing");
    }
    // clossing the doors when leaving the trigger
}

