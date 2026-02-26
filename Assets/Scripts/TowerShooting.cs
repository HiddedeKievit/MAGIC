using UnityEngine;

[RequireComponent(typeof(TowerTargeting))]
public class TowerShooting : MonoBehaviour
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private TowerTargeting targeting;
    private float fireCooldown;

    void Awake()
    {
        targeting = GetComponent<TowerTargeting>();
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Transform target = targeting.CurrentTarget;
        if (target == null)
            return;

        AimAtTarget(target);

        if (fireCooldown <= 0f)
        {
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
    }

    void AimAtTarget(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }

    void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        TowerAttack attack = projectile.GetComponent<TowerAttack>();
        if (attack != null)
        {
            attack.SetTarget(target);
        }
    }
}