using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 defaultStartPos;

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

     /*   switch (scene.buildIndex)
        {
            case 0: // First scene
                startPos = new Vector3(131.27f, 53.28f, 138.41f); // Example position
                break;
            case 1: // Second scene
                startPos = new Vector3(128.34f, 57.94f, 151.1f); // Example position
                break;
            default:
                Debug.Log("Defaut");
                startPos = defaultStartPos; // Use default start position
                break;
        }*/

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("Original Player Position: " + player.transform.position);
            player.transform.position = startPos;
            Debug.Log("Player Placed at: " + startPos);
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }
}
