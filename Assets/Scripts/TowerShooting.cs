using UnityEngine;

[RequireComponent(typeof(Tower))]
[RequireComponent(typeof(TowerTargeting))]
[RequireComponent(typeof(TowerAttack))]
public class TowerShooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    private Tower tower;
    private TowerTargeting targeting;
    private TowerAttack attack;

    private float fireCooldown;

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
            attack.Fire(firePoint, target);
            fireCooldown = 1f / tower.Data.fireRate;
        }
    }

    void AimAtTarget(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }
}