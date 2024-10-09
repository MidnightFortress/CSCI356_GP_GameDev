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
            // Find the Respawn object (or update it directly if it's attached to this checkpoint)
            GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");

            if (respawnObject != null)
            {
                // Update the respawn position to this checkpoint's position and rotation
                PlayerPlacer respawnPlacer = respawnObject.GetComponent<PlayerPlacer>();

                if (respawnPlacer != null)
                {
                    // Set the checkpoint's position and rotation as the new respawn point
                    respawnPlacer.SetRespawn();
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
        }
    }
}
