using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlacer : MonoBehaviour
{
    
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 defaultStartPos;
    private bool hasPlacedPlayer = false;

    private void OnEnable()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;
           
        
        // Subscribe to the sceneLoaded event
       
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!hasPlacedPlayer)
        {
            switch (scene.buildIndex)
            {
                case 0: // First scene
                    startPos = new Vector3(131.27f, 53.28f, 138.41f); // Example position
                    break;
                case 1: // Second scene
                    startPos = new Vector3(121.48f, 66.67f, 127.38f); // Example position
                    break;
                default:
                    Debug.Log("Default position applied.");
                    startPos = defaultStartPos; // Default start position if not specified
                    break;
            }

            Debug.Log("Scene loaded, starting player placement...");
            StartCoroutine(PlacePlayerAfterDelay());
        }
    }

    private IEnumerator PlacePlayerAfterDelay()
    {
        GameObject player = null;

        // Loop to wait until the player is found
        while (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                Debug.Log("Original Player Position before placement: " + player.transform.position);
                player.transform.position = startPos; // Set player position
                Debug.Log("Player Placed at: " + startPos);
                hasPlacedPlayer = true; // Set the flag so the placement only happens once

                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
            else
            {
                Debug.LogWarning("Player not found yet, retrying...");
            }

            yield return new WaitForSeconds(0.1f); // Retry every 0.1 seconds
        }
    }
}
