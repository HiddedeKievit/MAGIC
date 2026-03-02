using UnityEngine;

[RequireComponent(typeof(TowerTargeting))]
[RequireComponent(typeof(TowerAttack))]
public class TowerShooting : MonoBehaviour
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private Transform firePoint;

    private TowerTargeting targeting;
    private TowerAttack attack;
    private float fireCooldown;

    public float stunTimer = 0;

    void Awake()
    {
        targeting = GetComponent<TowerTargeting>();
        attack = GetComponent<TowerAttack>();
    }

    void Update()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            return;
        } else
        {
            stunTimer = 0;
        }

        fireCooldown -= Time.deltaTime;

        Transform target = targeting.CurrentTarget;
        if (target == null)
            return;

        AimAtTarget(target);

        if (fireCooldown <= 0f)
        {
            attack.Fire(firePoint, target);
            fireCooldown = 1f / fireRate;
        }
    }

    void AimAtTarget(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }
}