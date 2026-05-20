using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;


    private List<Entity> potentialResults;


    public List<ProjectileData> Projectiles;
    private Dictionary<Vector2Int, List<ProjectileData>> ProjectileGrid;

    private void Awake()
    {
        ProjectileGrid = new();
        potentialResults = new();
        if (Instance)
            Debug.LogWarning("double ProjectileManager instance detected");

        Instance = this;
    }


    private void Update()
    {

        for (int i = Projectiles.Count - 1; i >= 0; i--)
        {
            ProjectileData projectile = Projectiles[i];
            if (projectile.isDead)
            {
                if (ProjectileGrid.ContainsKey(projectile.gridPosition))
                    ProjectileGrid[projectile.gridPosition].Remove(projectile);

                Projectiles.RemoveAt(i);
                Destroy(projectile.gameObject);
                continue;
            }
        }

        if (Projectiles.Count == 0)
            return;

        foreach (ProjectileData projectile in Projectiles)
        {
            if (projectile == null)
                continue;

            // lifetime
            if (projectile.Lifetime > projectile.Timer)
            {
                projectile.Timer += Time.deltaTime;
            } else
            {
                projectile.isDead = true;
            }


            // movement
            projectile.transform.position += projectile.direction * projectile.Speed * Time.deltaTime;


            // insert code later

            // update grid
            int centerX = Mathf.FloorToInt(projectile.transform.position.x / LevelData.Instance.GridCellSize);
            int centerY = Mathf.FloorToInt(projectile.transform.position.y / LevelData.Instance.GridCellSize);

            Vector2Int newKey = new(centerX, centerY);

            // if the position changed, move enemy grid cell.
            if (newKey != projectile.gridPosition)
            {
                // if the grid position doesnt exist, create it.
                MoveProjectile(projectile, newKey);
            }


            // one projectile can now deal damage to one enemy twice if the piercing is more than 1. there needs to be some way to track if the projectile is currently in an enemy, only on first hit deal damage.
            // this is where "on collide" and "colliding" would be better, oh well...
            // perhaps manually somehow. but then the case of 2 overlapping enemies would result in a flipflop between them. this may actually be fixable if enemies are always seperated a bit so their "collision" doesnt overlap. 

            // projectile states maybe? "hit?" "colliding" "flying?"

            List<Entity> enemiesInCell = EnemyManager.Instance.GetCellList(projectile.gridPosition);
            // TowerManager.Instance.GetOverlappingTower(GhostTower.transform.position, TowerToPlace.size)
            if (enemiesInCell != null)
            {
                for (int e = enemiesInCell.Count - 1; e >= 0; e--)
                {
                    Entity enemy = enemiesInCell[e];
                    if (( enemy.transform.position - projectile.transform.position ).sqrMagnitude < projectile.transform.localScale.x)
                    {
                        enemy.Health -= projectile.Damage;

                        projectile.Piercing--;
                        if (projectile.Piercing <= 0)
                        {
                            projectile.isDead = true;
                            break; // break loop for this projectile because its dead, done, gone, over, did its job!
                        }
                    }
                }
            }

        }
    }

    public void SpawnProjectile(ProjectileData projectilePrefab, TowerData origin, Entity target)
    {
        Vector3 dir = ( target.transform.position - origin.transform.position ).normalized;

        ProjectileData projectile = Instantiate(projectilePrefab, origin.transform.position, Quaternion.Euler(0, 0, ( Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg )), transform);
        projectile.direction = dir;
        int centerX = Mathf.FloorToInt(projectile.transform.position.x / LevelData.Instance.GridCellSize);
        int centerY = Mathf.FloorToInt(projectile.transform.position.y / LevelData.Instance.GridCellSize);

        Vector2Int newKey = new(centerX, centerY);

        MoveProjectile(projectile, newKey);

        Projectiles.Add(projectile);
    }

    public void MoveProjectile(ProjectileData projectile, Vector2Int newPos)
    {
        if (ProjectileGrid.ContainsKey(projectile.gridPosition))
            ProjectileGrid[projectile.gridPosition].Remove(projectile);

        if (!ProjectileGrid.ContainsKey(newPos))
        {
            ProjectileGrid[newPos] = new();
        }
        ProjectileGrid[newPos].Add(projectile);
        projectile.gridPosition = newPos;
    }
}