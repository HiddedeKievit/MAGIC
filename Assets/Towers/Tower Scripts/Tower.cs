using UnityEngine;

public class Tower : MonoBehaviour
{
    public TurretData Data { get; private set; }

    // Runtime stats (VERY IMPORTANT)
    public float Range { get; private set; }
    public float FireRate { get; private set; }
    public int Damage { get; private set; }

    private TowerAttackBehaviour attackBehaviour;

    public void Initialize(TurretData data)
    {
        Data = data;

        Range = data.range;
        FireRate = data.fireRate;
        Damage = data.damage;

        attackBehaviour = Instantiate(data.attackBehaviour);
        attackBehaviour.Initialize(this);
    }

    public void Attack(Transform target)
    {
        if (attackBehaviour == null || target == null) return;

        attackBehaviour.Execute(target);
    }

    // Upgrade hooks
    public void AddRange(float amount) => Range += amount;
    public void AddDamage(int amount) => Damage += amount;
    public void MultiplyFireRate(float multiplier) => FireRate *= multiplier;

    public void SetAttackBehaviour(TowerAttackBehaviour newAttack)
    {
        attackBehaviour = Instantiate(newAttack);
        attackBehaviour.Initialize(this);
    }

    // Used by behaviours
    public void SpawnProjectile(Transform target)
    {
        GameObject proj = Instantiate(
            Data.projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        proj.GetComponent<Projectile>()
            .Initialize(target, Damage, Data.projectileSpeed);
    }
}