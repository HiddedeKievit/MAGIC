using UnityEngine;

public class TowerShooting : MonoBehaviour
{
    public TurretData data;
    public TowerTargeting targeting;

    public Transform firePoint;

    private float fireCooldown;

    private void Update()
    {
        if (targeting.currentTarget == null) return;

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / data.fireRate;
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(
            data.projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.Initialize(targeting.currentTarget, data);
    }
}