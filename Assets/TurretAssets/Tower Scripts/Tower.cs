public class Tower : MonoBehaviour
{
    public TurretData Data { get; private set; }

public void AddRange(float amount) => Range += amount;
public void AddDamage(int amount) => Damage += amount;
public void MultiplyFireRate(float multiplier) => FireRate *= multiplier;

public void SetAttackBehaviour(TowerAttackBehaviour newAttack)
{
    attackBehaviour = Instantiate(newAttack);
    attackBehaviour.Initialize(this);
}

    public void Initialize(TurretData data)
    {
        Data = data;

        Range = data.range;
        FireRate = data.fireRate;
        Damage = data.damage;

        // Inject attack behavior
        attackBehaviour = Instantiate(data.attackBehaviour);
        attackBehaviour.Initialize(this);
    }

    public void Attack(Transform firePoint, Transform target)
    {
        attackBehaviour.ExecuteAttack(firePoint, target);
    }
}