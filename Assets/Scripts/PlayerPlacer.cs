using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPlacer : MonoBehaviour
{
    
     private Vector3 startPos;
    [SerializeField] private Vector3 defaultStartPos;

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

        Debug.Log("on Scene");

        startPos = transform.position;

        Debug.Log("StartPos from attached object: " + startPos);
       
        StartCoroutine(PlacePlayerAfterDelay());
        
    }

    private IEnumerator PlacePlayerAfterDelay()
    {
        yield return null;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            // Log the player's original position
            Debug.Log("Original Player Position before placement: " + player.transform.position);

            // Set the player's position to the startPos (the position of the GameObject this script is attached to)
            player.transform.position = startPos;

            // Log the new player position
            Debug.Log("Player Placed at: " + startPos);
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }

}
