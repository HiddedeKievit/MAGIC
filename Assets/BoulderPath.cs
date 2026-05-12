using UnityEngine;

public class BoulderPath : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Boulder Health")]
    [SerializeField] private int health = 25;

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
        }

        // Start at end of path
        waypointIndex = waypoints.Length - 1;

        transform.position = waypoints[waypointIndex].position;

        // Move backward through path
        waypointIndex--;
    }

    private void Update()
    {
        if (waypoints == null)
            return;

        // Boulder destroyed
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // Reached end of path
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

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.05f)
        {
            waypointIndex--;
        }

        // Rolling effect
        Vector2 moveDirection = (
    targetWaypoint.position - transform.position
).normalized;

float rotationDirection = moveDirection.x > 0 ? -1f : 1f;

transform.Rotate(0f, 0f, rotationDirection * 360f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    Entity entity = collision.GetComponent<Entity>();

    if (entity != null)
    {
        int enemyHealth = entity.Health;
        int boulderHealth = health;

        // Damage enemy by current boulder HP
        entity.Health -= boulderHealth;

        // Damage boulder by current enemy HP
        health -= enemyHealth;

        Debug.Log("Boulder HP Remaining: " + health);
    }
}
}