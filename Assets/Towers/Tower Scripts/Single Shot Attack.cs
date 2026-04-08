using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Attack/Single Shot")]
public class SingleShotAttack : TowerAttackBehaviour
{
    public override void ExecuteAttack(Transform firePoint, Transform target)
    {
        GameObject projectile = Instantiate(
            tower.Data.projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        projectile.GetComponent<Projectile>()
            .Initialize(target, tower.Damage, tower.Data.projectileSpeed);
    }
}