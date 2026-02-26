using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private float range = 5f;

    private Transform target;

    public Transform CurrentTarget => target;

    void Update()
    {
        // Assign a target if there isn’t one
        if (target == null)
        {
            FindTarget();
        }

        // If we have a target, aim at it
        if (target != null)
        {
            AimAtTarget();

            // If target moves out of range, forget it
            if (Vector2.Distance(transform.position, target.position) > range)
            {
                target = null;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance <= range)
            {
                target = enemy.transform;
                return; // pick the first enemy in range
            }
        }

        target = null;
    }

    void AimAtTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust -90 if sprite faces up
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }

    // Optional: visualize the range in the scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}