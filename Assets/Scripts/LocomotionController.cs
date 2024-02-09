using UnityEngine;
using UnityEngine.AI;

public class LocomotionController : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the waypoints
    [SerializeField] private int currentWaypointIndex = 0; // Index of the current waypoint
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public float idleDuration = 10f; // Duration in seconds to idle at each waypoint
    private float idleTimer = 0f;
    private bool isIdling = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Check if there are any waypoints assigned
        if (waypoints.Length > 0)
        {
            // If the NavMeshAgent has a path and is close to the current destination, proceed to the next waypoint
            if (navMeshAgent.hasPath && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }

            SetDestinationToWaypoint();
        }
        else
        {
            Debug.LogError("No waypoints assigned to NPCMovement script on " + gameObject.name);
        }
    }


    void Update()
    {
        // Check if the NPC has reached the current waypoint
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!isIdling)
            {
                // Start idling
                animator.SetFloat("Speed", 0f); // Set speed parameter to 0 to transition to idle
                isIdling = true;
                idleTimer = 0f;
            }
            else
            {
                // Increment idle timer
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleDuration)
                {
                    // Stop idling and proceed to the next waypoint
                    isIdling = false;
                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                    SetDestinationToWaypoint();
                }
            }
        }
        else
        {
            // NPC is moving, set speed parameter to 1 for movement animation
            animator.SetFloat("Speed", 1f);
        }
    }

    void SetDestinationToWaypoint()
    {
        // Set the destination of the NavMeshAgent to the current waypoint
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }


}
