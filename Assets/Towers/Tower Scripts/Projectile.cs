using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private int damage;

    public void Initialize(Transform target, TurretData data)
    {
        this.target = target;
        this.speed = data.projectileSpeed;
        this.damage = data.damage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        // Assumes enemy has a health script
        other.GetComponent<EnemyHealth>()?.TakeDamage(damage);

        Destroy(gameObject);
    }
}