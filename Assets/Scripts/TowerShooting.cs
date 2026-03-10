using UnityEngine;

[RequireComponent(typeof(Tower))]
[RequireComponent(typeof(TowerTargeting))]
[RequireComponent(typeof(TowerAttack))]
public class TowerShooting : MonoBehaviour
{
[SerializeField] private Transform firePoint;
[SerializeField] private Transform firePointLeft;
[SerializeField] private Transform firePointRight;

    private Tower tower;
    private TowerTargeting targeting;
    private TowerAttack attack;

    private float fireCooldown;

    private bool fireLeftNext = true;

    void Awake()
    {
        tower = GetComponent<Tower>();
        targeting = GetComponent<TowerTargeting>();
        attack = GetComponent<TowerAttack>();
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
if (tower.Path2Level > 0)
{
    if (fireLeftNext)
        attack.Fire(firePointLeft, target);
    else
        attack.Fire(firePointRight, target);

    fireLeftNext = !fireLeftNext;
}
else
{
    attack.Fire(firePoint, target);
}
            fireCooldown = 1f / tower.CurrentFireRate;
        }
    }

    void AimAtTarget(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }
}