using UnityEngine;

public class TowerTargeting : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private float range = 5f;
    public float Range => range;

    private Transform target;
    public Transform CurrentTarget => target;

    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }

        if (target != null)
        {
            // If target moves out of range, forget it
            if (Vector2.Distance(transform.position, target.position) > range)
            {
                target = null;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject entity in entities)
        {
            float distance = Vector2.Distance(transform.position, entity.transform.position);

            if (distance <= range)
            {
                target = entity.transform;
                return;
            }
        }

        target = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}