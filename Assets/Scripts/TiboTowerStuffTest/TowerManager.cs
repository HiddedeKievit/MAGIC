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
        // if no towers, do nothing
        if (Towers.Count == 0)
            return;

        // go trough all towers
        foreach (TowerData tower in Towers)
        {
            // if i have a target, look at it
            if (tower.currentTarget != null)
            {
                Vector3 dir = tower.currentTarget.transform.position - tower.transform.position;
                float dist = dir.sqrMagnitude;
                if (dist < tower.range * tower.range)
                {
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (tower.turret != null)
            {
                tower.turret.localRotation =
                Quaternion.Euler(0, 0, angle);
            }
                }
            }

            // if my cooldown is active skip the rest 
            if (tower.cooldown > 0)
            {
                tower.cooldown -= Time.deltaTime;

                continue;
            }

            // check if activeprojectile has null references (projectile was killed)
            for (int projectileIndex = 0; projectileIndex < tower.activeProjectiles.Count; projectileIndex++)
            {
                if (tower.activeProjectiles[projectileIndex] == null)
                    tower.activeProjectiles.RemoveAt(projectileIndex);
            }

            // check if i can have another projectile
            if (tower.activeProjectiles.Count >= tower.maxProjectiles)
                continue;


            // reset cooldown and target
            tower.cooldown = tower.firerate;
            tower.currentTarget = null;

            // if the projectile should be circling the tower set target as self, else run targeting
            if (tower.projectileCircleMe)
            {
                tower.currentTarget = tower.transform;
            } else
            {
                // get enemies in cells in range
                EnemyManager.Instance.GetEnemiesAt(tower.transform.position.x, tower.transform.position.y, tower.range, potentialResults);

                float rangeSqr = tower.range * tower.range;
                Vector3 towerPos = tower.transform.position;

                // kick out any enemy thats not in shooting range
                potentialResults.RemoveAll(enemy => ( enemy.transform.position - towerPos ).sqrMagnitude > rangeSqr);

                // make sure theres at least 1 target.
                if (potentialResults.Count == 0)
                    continue;

                // who do i shoot
                switch (tower.targeting)
                {
                    // go trough all potential results and get the closest
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
                        tower.currentTarget = closest.transform;

                        break;

                    // go trough all potential results and get the farthest
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
                        tower.currentTarget = farthest.transform;
                        break;
                }

                // TODO Weak
                // TODO Strong
                // TODO Progress First
                // TODO Profress Last
            }

            // if there is a target, spawn projectile
            if (tower.currentTarget)
            {
                ProjectileData shot = ProjectileManager.Instance.SpawnProjectile(tower.projectile, tower, tower.currentTarget);

                tower.activeProjectiles.Add(shot);
            }
        }
    }

    public TowerData SpawnTower(TowerData towerPrefab, Vector3 position)
    {
        // spawn a tower at position with rotation and use me as parent
        TowerData tower = Instantiate(towerPrefab, position, towerPrefab.transform.rotation, transform);
        Towers.Add(tower);

        return tower;
    }

    public TowerData GetOverlappingTower(Vector2 position, Vector2 hitbox)
    {
        // you know the drill (check GetOverlappingX on other managers)
        Rect a = RectFromCenter(position, hitbox);

        foreach (TowerData tower in Towers)
        {
            Rect b = RectFromCenter(tower.transform.position, tower.hitbox);

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