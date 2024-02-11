using UnityEngine;
using UnityEngine.AI;

public enum WaypointSelectionMode
{
    Random,
    Sequential
}

public class LocomotionController : MonoBehaviour
{
    public WaypointSelectionMode waypointSelectionMode = WaypointSelectionMode.Sequential; // Enum to choose waypoint selection mode
    public Waypoint[] waypoints; // Array to hold the waypoints
    [SerializeField] private int currentWaypointIndex = 0; // Index of the current waypoint
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Waypoint currentWaypoint;

    private float idleTimer = 0f;
    private bool isIdling = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (waypoints.Length > 0)
        {
            SetNextWaypointIndex();
            SetDestinationToWaypoint();
        }
        else
        {
            Debug.LogError("No waypoints assigned to LocomotionController script on " + gameObject.name);
        }
    }

    void Update()
    {
        if (!isIdling && !currentWaypoint.IsOccupied())
        {
            currentWaypoint.Occupy(gameObject);
        }
        else if (!isIdling && currentWaypoint.IsOccupied() && currentWaypoint.GetVisitingNPC() != gameObject)
        {
            // Find another unoccupied waypoint
            FindUnoccupiedWaypoint();
        }

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!isIdling)
            {
                animator.SetFloat("Speed", 0f);
                isIdling = true;
                idleTimer = 0f;
            }
            else
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= currentWaypoint.idleDuration)
                {
                    isIdling = false;
                    // Release the current waypoint
                    currentWaypoint.Release();
                    SetNextWaypointIndex();
                    SetDestinationToWaypoint();
                }
            }
        }
        else
        {
            animator.SetFloat("Speed", 1f);
        }
    }

    void SetDestinationToWaypoint()
    {
        currentWaypoint = waypoints[currentWaypointIndex];
        navMeshAgent.SetDestination(currentWaypoint.transform.position);
    }

    void SetNextWaypointIndex()
    {
        if (waypointSelectionMode == WaypointSelectionMode.Sequential)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else if (waypointSelectionMode == WaypointSelectionMode.Random)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Length);
        }
    }

    void FindUnoccupiedWaypoint()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (!waypoints[i].IsOccupied())
            {
                currentWaypoint.Release();
                currentWaypointIndex = i;
                SetDestinationToWaypoint();
                return;
            }
        }
    }
}
