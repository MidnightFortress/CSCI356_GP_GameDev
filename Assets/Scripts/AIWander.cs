using UnityEngine;
using UnityEngine.AI;

public class AIWander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float sizeChangeSpeed = 1f;   // How fast the size transition happens
    public float minSize = 0.5f;         // Minimum size on X and Z axes
    public float maxSize = 2f;           // Maximum size on X and Z axes
    public float transitionDuration = 2f; // Time for size transition

    public bool enableSizeChange = true;  // Toggle for size change feature

    private NavMeshAgent agent;
    private float timer;
    private Vector3 targetScale;         // Target scale for smooth transition
    private bool isTransitioning = false;
    private float transitionProgress = 0f;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        // Set initial target scale to current scale
        targetScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = GetWanderDirection();
            agent.SetDestination(newPos);

            // Initiate smooth size transition if the feature is enabled
            if (enableSizeChange)
            {
                SetNewTargetScale();
                isTransitioning = true;
                transitionProgress = 0f; // Reset the transition progress
            }

            timer = 0;
        }

        // Smoothly update the scale during the transition if the feature is enabled
        if (isTransitioning && enableSizeChange)
        {
            SmoothSizeTransition();
        }
    }

    // Get a random direction restricted to 90-degree angles
    Vector3 GetWanderDirection()
    {
        Vector3 currentPos = transform.position;

        // Randomly choose between 4 cardinal directions: 0, 90, 180, and 270 degrees
        int randomAngle = Random.Range(0, 4) * 90;

        // Convert the angle to a direction vector
        Vector3 direction = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;

        // Calculate a new target position using the chosen direction and wander radius
        Vector3 targetPosition = currentPos + direction * wanderRadius;

        // Ensure it's a valid point on the NavMesh
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(targetPosition, out navHit, wanderRadius, -1))
        {
            return navHit.position;
        }

        return currentPos; // Fallback in case no valid point is found
    }

    // Function to set a new target scale if size change is enabled
    void SetNewTargetScale()
    {
        float newX = Random.Range(minSize, maxSize); // Random new X size
        float newZ = Random.Range(minSize, maxSize); // Random new Z size

        // Set the target scale while keeping the Y axis the same
        targetScale = new Vector3(newX, transform.localScale.y, newZ);
    }

    // Smoothly transitions the size to the target scale over time
    void SmoothSizeTransition()
    {
        // Increment the progress based on time and speed
        transitionProgress += Time.deltaTime / transitionDuration;

        // Interpolate between the current scale and the target scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, transitionProgress);

        // Check if transition is complete
        if (transitionProgress >= 1f)
        {
            isTransitioning = false;
        }
    }
}
