using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float projectileSpeed = 60f;
    [SerializeField] private GameObject projectilePrefab;

    public void Fire(Transform firePoint, Transform target)
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(target, damage, projectileSpeed);
        }
    }
}