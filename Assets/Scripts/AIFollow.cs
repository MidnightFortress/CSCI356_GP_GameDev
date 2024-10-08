using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float followRange = 15f;    // The range in which the AI detects the player
    public float stopDistance = 10f;   // The minimum distance to maintain from the player

    private NavMeshAgent agent;
    private float timer;
    private Transform player;
    private bool playerInRange = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has the "Player" tag
        timer = wanderTimer;
    }

    void Update()
    {
        // Check if player is within range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange)
        {
            FollowPlayer(distanceToPlayer);
        }
        else
        {
            WanderAround();
        }
    }

    void FollowPlayer(float distanceToPlayer)
    {
        // If the AI is further than the stopDistance, move towards the player
        if (distanceToPlayer > stopDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            // If within the stopDistance, stop moving (but stay close)
            agent.SetDestination(transform.position);
        }
    }

    void WanderAround()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    // Get a random position within a certain radius on the NavMesh
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}

