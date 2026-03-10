using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private int damage;
    private float speed;

    public void Initialize(Transform newTarget, int newDamage, float newSpeed)
    {
        target = newTarget;
        damage = newDamage;
        speed = newSpeed;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If we reach the enemy this frame
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Entity entity = target.GetComponent<Entity>();

        if (entity != null)
        {
            entity.Health -= damage;
        }

        Destroy(gameObject);
    }
}