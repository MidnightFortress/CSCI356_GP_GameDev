using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of objects to spawn
    public float[] spawnChances; // Corresponding spawn chances for each object

    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float followRange = 15f;
    public float stopDistance = 10f;

    public void SpawnObject()
    {
        // Validate input
        if (objectsToSpawn.Length != spawnChances.Length)
        {
            Debug.LogError("Object array and chance array must have the same length.");
            return;
        }

        // Calculate total spawn chance
        float totalChance = 0f;
        for (int i = 0; i < spawnChances.Length; i++)
        {
            totalChance += spawnChances[i];
        }

        // Generate a random number between 0 and the total chance
        float randomValue = Random.Range(0f, totalChance);

        // Determine the object to spawn based on the random value
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            if (randomValue <= totalChance)
            {
                // Spawn the object at the spawner's position and rotation
                GameObject spawnedObject = Instantiate(objectsToSpawn[i], transform.position, transform.rotation);

                // Check if the spawned object has the AIFollow script attached
                AIFollow aiFollowScript = spawnedObject.GetComponent<AIFollow>();

                if (aiFollowScript != null)
                {
                    // Assign values from spawner to AIFollow script
                    aiFollowScript.wanderRadius = wanderRadius;
                    aiFollowScript.wanderTimer = wanderTimer;
                    aiFollowScript.followRange = followRange;
                    aiFollowScript.stopDistance = stopDistance;
                }
                else
                {
                    Debug.LogError("Spawned object doesn't have AIFollow script attached!");
                }

                return;
            }

            randomValue -= spawnChances[i];
        }

        // If no object was spawned, log an error
        Debug.LogError("No object was spawned. Please check your spawn chances.");
    }
}