using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public List<TowerData> Towers;

    private List<Entity> potentialResults;

    private void Awake()
    {
        Instance = this;
        potentialResults = new();
    }


    void Update()
    {

        foreach (TowerData tower in Towers)
        {
            if (tower.currentTarget != null)
            {
                Vector3 dir = tower.currentTarget.transform.position - tower.transform.position;
                float dist = dir.sqrMagnitude;
                if (dist < tower.range * tower.range)
                {
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    tower.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }

            // cooldown
            if (tower.cooldown > 0)
            {
                tower.cooldown -= Time.deltaTime;
                // skip rest of code
                continue;
            }



            // get enemies in range
            EnemyManager.Instance.GetEnemiesAt(tower.transform.position.x, tower.transform.position.y, tower.range, potentialResults);

            float rangeSqr = tower.range * tower.range;
            Vector3 towerPos = tower.transform.position;

            // kick out any enemy thats not in range
            potentialResults.RemoveAll(enemy => ( enemy.transform.position - towerPos ).sqrMagnitude > rangeSqr);

            // make sure theres at least 1 target.
            if (potentialResults.Count == 0)
                continue;

            tower.cooldown = tower.firerate;

            // targeting and shoot.
            switch (tower.targeting)
            {
                case Targeting.Close:
                    Entity closest = null;
                    float minDistance = float.MaxValue;

                    foreach (Entity enemy in potentialResults)
                    {
                        // skipping squareroot calculations that "Distance" function would use.
                        float dist = ( enemy.transform.position - tower.transform.position ).sqrMagnitude;

                        // check if enemy is closer than another
                        if (dist < minDistance)
                        {
                            minDistance = dist;
                            closest = enemy;
                        }
                    }

                    // target is now "closest"
                    tower.currentTarget = closest;
                    ProjectileManager.Instance.SpawnProjectile(tower.projectile, tower, closest);

                    break;

            }



            // shooting
            // extra effects?
        }
    }

}