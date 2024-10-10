using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
            // finding the player placer

            if (respawnObject != null)
            {
                PlayerPlacer respawnPlacer = respawnObject.GetComponent<PlayerPlacer>();

                if (respawnPlacer != null)
                {
                    respawnPlacer.SetRespawn();
                    // this is changing the respawn position.
                    // you will notice in the PlayerPlace i hardcoded the position instead of dinding the startPos
                    // it sbecause it wasnt working and it was annoying me so i just hardcoded it. only need one change so it doesnt matter
                    Debug.Log("Respawn point updated to: " + respawnPlacer.transform.position);
                }
                else
                {
                    Debug.LogError("PlayerPlacer script not found on Respawn object.");
                }
            }
            else
            {
                Debug.LogError("Respawn object not found.");
            }
            // fuckload of debugs
        }
    }
}
