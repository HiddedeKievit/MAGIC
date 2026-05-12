using UnityEngine;

public class BoulderPath : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Damage")]
    [SerializeField] private int damage = 25;

    private Transform[] waypoints;
    private int waypointIndex;

    private void Start()
    {
        GameObject path = GameObject.Find("path");

        if (path == null)
        {
            Debug.LogError("Path object not found!");
            enabled = false;
            return;
        }

        int childCount = path.transform.childCount;

        if (childCount == 0)
        {
            Debug.LogError("Path has no waypoints!");
            enabled = false;
            return;
        }

        waypoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            waypoints[i] = path.transform.GetChild(i);

            if (waypoints[i] == null)
            {
                Debug.LogError($"Waypoint {i} is null!");
            }
        }

        // Start at last waypoint
        waypointIndex = waypoints.Length - 1;

        transform.position = waypoints[waypointIndex].position;

        // Move backwards
        waypointIndex--;
    }

    private void Update()
    {
        if (waypoints == null)
            return;

        if (waypointIndex < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (waypoints[waypointIndex] == null)
        {
            Debug.LogError($"Waypoint at index {waypointIndex} is null!");
            return;
        }

        Transform targetWaypoint = waypoints[waypointIndex];

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetWaypoint.position,
            moveSpeed * Time.deltaTime
        );

        float distance = Vector2.Distance(
            transform.position,
            targetWaypoint.position
        );

        if (distance < 0.05f)
        {
            waypointIndex--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();

        if (entity != null)
        {
            entity.Health -= damage;
        }
    }
}