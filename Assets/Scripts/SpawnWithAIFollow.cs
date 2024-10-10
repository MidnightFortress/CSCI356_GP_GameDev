using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnWithAIFollow : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn; // List of prefabs to spawn with AI

    // Public variables to set AIFollow and NavMeshAgent properties
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float followRange = 15f;
    public float stopDistance = 10f;
    public float respawnTimer = 10f; // Time between respawns

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Choose a random prefab from the list
            int randomIndex = Random.Range(0, prefabsToSpawn.Count);
            GameObject prefabToSpawn = prefabsToSpawn[randomIndex];

            // Spawn the chosen prefab at the current gameObject's position and rotation
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);

            // Attach NavMeshAgent to the spawned object
            NavMeshAgent navMeshAgent = spawnedObject.AddComponent<NavMeshAgent>();
            navMeshAgent.speed = 5f; // Set NavMeshAgent speed (adjust as needed)

            // Attach AIFollow script to the spawned object
            AIFollow aiFollowScript = spawnedObject.AddComponent<AIFollow>();

            // Assign values from spawner to AIFollow script
            aiFollowScript.wanderRadius = wanderRadius;
            aiFollowScript.wanderTimer = wanderTimer;
            aiFollowScript.followRange = followRange;
            aiFollowScript.stopDistance = stopDistance;

            // Wait for respawn timer before spawning the next object
            yield return new WaitForSeconds(respawnTimer);
        }
    }
}