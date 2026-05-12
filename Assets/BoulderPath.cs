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
        GameObject path = GameObject.Find("Path");

        if (path == null)
        {
            Debug.LogError("No object named 'Path' found!");
            return;
        }

        int childCount = path.transform.childCount;

        waypoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            waypoints[i] = path.transform.GetChild(i);
        }

        // Start at LAST waypoint
        waypointIndex = waypoints.Length - 1;

        transform.position = waypoints[waypointIndex].position;

        // Begin moving backward
        waypointIndex--;
    }

    private void Update()
    {
        // Finished path
        if (waypointIndex < 0)
        {
            Destroy(gameObject);
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