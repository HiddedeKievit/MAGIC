using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TowerTargeting : MonoBehaviour
{
    private Tower tower;

    private Transform target;
    public Transform CurrentTarget => target;

    public float Range
{
    get
    {
        if (tower == null || tower.Data == null)
            return 0f;

        return tower.Data.range;
    }
}

    void Awake()
    {
        tower = GetComponent<Tower>();
    }

    void Update()
    {
        if (target == null)
            FindTarget();

        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) > Range)
                target = null;
        }
    }

    void FindTarget()
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject entity in entities)
        {
            float distance = Vector2.Distance(transform.position, entity.transform.position);

            if (distance <= Range)
            {
                target = entity.transform;
                return;
            }
        }

        target = null;
    }

    void OnDrawGizmosSelected()
    {
        if (tower == null || tower.Data == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tower.Data.range);
    }
}