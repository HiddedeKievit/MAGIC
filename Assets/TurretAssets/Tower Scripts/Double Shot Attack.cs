using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Attack/Double Shot")]
public class DoubleShotAttack : TowerAttackBehaviour
{
    private bool fireLeft = true;

    public override void ExecuteAttack(Transform target)
    {
        Transform firePoint = fireLeft ? tower.FirePointLeft : tower.FirePointRight;

        GameObject projectile = Instantiate(
            tower.Data.projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        projectile.GetComponent<Projectile>()
            .Initialize(target, tower.Damage, tower.Data.projectileSpeed);

        fireLeft = !fireLeft;
    }
}