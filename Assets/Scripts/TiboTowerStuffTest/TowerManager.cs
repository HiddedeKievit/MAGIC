using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public List<TowerData> Towers;

    private List<Entity> potentialResults;

    private void Awake()
    {
        Towers = new();
        Instance = this;
        potentialResults = new();
    }


    void Update()
    {
        if (Towers.Count == 0)
            return;

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

            tower.currentTarget = null;
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

                    break;
                case Targeting.Far:
                    Entity farthest = null;
                    float maxDistance = 0;

                    foreach (Entity enemy in potentialResults)
                    {
                        // skipping squareroot calculations that "Distance" function would use.
                        float dist = ( enemy.transform.position - tower.transform.position ).sqrMagnitude;

                        // check if enemy is farther than another
                        if (dist > maxDistance)
                        {
                            maxDistance = dist;
                            farthest = enemy;
                        }
                    }

                    // target is now "farthest"
                    tower.currentTarget = farthest;
                    break;

            }

            // if there is a target, shoot it.
            if (tower.currentTarget)
                ProjectileManager.Instance.SpawnProjectile(tower.projectile, tower, tower.currentTarget);
        }
    }

    public TowerData SpawnTower(TowerData towerPrefab, Vector3 position)
    {
        TowerData tower = Instantiate(towerPrefab, position, towerPrefab.transform.rotation, transform);
        Towers.Add(tower);

        return tower;
    }

    public TowerData GetOverlappingTower(Vector2 position, Vector2 size)
    {
        Rect a = RectFromCenter(position, size);

        foreach (TowerData tower in Towers)
        {
            Rect b = RectFromCenter(tower.transform.position, tower.size);

            if (a.Overlaps(b))
                return tower;
        }

        return null;
    }
    private Rect RectFromCenter(Vector2 center, Vector2 size)
    {
        return new Rect(
            center.x - size.x / 2f,
            center.y - size.y / 2f,
            size.x,
            size.y
        );
    }

}