using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    public TurretData data;

    public Transform currentTarget;

    private List<Transform> enemiesInRange = new List<Transform>();

    private void Update()
    {
        FindTarget();
        RotateToTarget();
    }

    void FindTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (var enemy in enemiesInRange)
        {
            if (enemy == null) continue;

            float distance = Vector2.Distance(transform.position, enemy.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = enemy;
            }
        }

        currentTarget = nearest;
    }

    void RotateToTarget()
    {
        if (currentTarget == null) return;

        Vector2 direction = currentTarget.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Add(other.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Remove(other.transform);
    }
}