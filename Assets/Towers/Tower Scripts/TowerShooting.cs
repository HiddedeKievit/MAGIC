using UnityEngine;

[RequireComponent(typeof(Tower))]
[RequireComponent(typeof(TowerTargeting))]
public class TowerShooting : MonoBehaviour
{
    private Tower tower;
    private TowerTargeting targeting;

    public float stunTimer = 0;
    private float fireCooldown;

    void Awake()
    {
        tower = GetComponent<Tower>();
        targeting = GetComponent<TowerTargeting>();
    }

    void Update()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            return;
        }

        fireCooldown -= Time.deltaTime;

        Transform target = targeting.CurrentTarget;
        if (target == null)
            return;

        AimAtTarget(target);

        if (fireCooldown <= 0f)
        {
            tower.Attack(target);
            fireCooldown = 1f / tower.FireRate;
        }
    }

    void AimAtTarget(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }
}