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
                Debug.Log($"[{projectile.gameObject.name}] DIED VIA TIMER. Timer: {projectile.Timer}, Lifetime: {projectile.Lifetime}");
                projectile.isDead = true;
            }

            if (projectile.isHomingTarget && projectile.target != null)
            {
                Vector3 diff = projectile.target.transform.position - projectile.transform.position;
                Vector3 dir = diff.normalized;
                float rotateSpeed = projectile.Speed * Time.deltaTime;

                if (projectile.isCirlingTarget)
                {
                    rotateSpeed /= 2;
                }

                projectile.transform.rotation = Quaternion.Slerp(
                    projectile.transform.rotation,
                    Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg),
                    rotateSpeed
                    );
            }


            // movement
            projectile.transform.position += projectile.Speed * Time.deltaTime * projectile.transform.right;

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


            // one projectile can now deal damage to one enemy twice if the piercing is more than 1.
            // there needs to be some way to track if the projectile is currently in an enemy, only on first hit deal damage.
            // this is where "on collide" and "colliding" would be better, oh well...
            // perhaps manually somehow. but then the case of 2 overlapping enemies would result in a flipflop between them.

            // this may actually be fixable if enemies are always seperated a bit so their "collision" doesnt overlap. 
            // or just adding a list of colliding enemies to projectiles, but then we could have many many lists in a lot of projectiles.


            EnemyManager.Instance.GetOverlappingEnemies(projectile.transform.position, projectile.hitbox, potentialResults);

            // is there any enemy
            if (potentialResults != null)
            {
                // every enemy i hit
                for (int e = potentialResults.Count - 1; e >= 0; e--)
                {
                    // deal damage
                    potentialResults[e].Health -= projectile.Damage;

                    // remove piercing
                    projectile.Piercing--;

                    // if no more piercing left, its its dead, done, gone, over, did its job!
                    if (projectile.Piercing <= 0)
                    {
                        Debug.Log($"[{projectile.gameObject.name}] DIED VIA PIERCING. Piercing: {projectile.Piercing}");
                        projectile.isDead = true;
                        break;
                    }
                }
            }
        }
    }

    public ProjectileData SpawnProjectile(ProjectileData projectilePrefab, TowerData origin, Transform target)
    {
        // get the direction from origin to target;
        Vector3 dir = ( target.transform.position - origin.transform.position ).normalized;

        // create projectile and add it to the grid
        ProjectileData projectile = Instantiate(projectilePrefab, origin.transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg), transform);
        projectile.target = target;

        int centerX = Mathf.FloorToInt(projectile.transform.position.x / LevelData.Instance.GridCellSize);
        int centerY = Mathf.FloorToInt(projectile.transform.position.y / LevelData.Instance.GridCellSize);

        Vector2Int newKey = new(centerX, centerY);

        MoveProjectile(projectile, newKey);

        Projectiles.Add(projectile);

        return projectile;
    }

    public void MoveProjectile(ProjectileData projectile, Vector2Int newPos)
    {
        // check if the projectile grid has a key for the active position, if so remove projectile from that position.
        if (ProjectileGrid.ContainsKey(projectile.gridPosition))
            ProjectileGrid[projectile.gridPosition].Remove(projectile);

        // if grid doesnt have a key for a position, make it.
        if (!ProjectileGrid.ContainsKey(newPos))
        {
            ProjectileGrid[newPos] = new();
        }

        // add projectile to the position and update reference
        ProjectileGrid[newPos].Add(projectile);
        projectile.gridPosition = newPos;
    }
}