using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;



    public List<Entity> Enemies;
    private Dictionary<Vector2Int, List<Entity>> EnemyGrid;

    private void Awake()
    {
        EnemyGrid = new();
        Instance = this;
    }


    public void GetEnemiesAt(float worldX, float worldY, float cellRange, List<Entity> results)
    {
        // clear list
        results.Clear();

        int centerX = Mathf.FloorToInt(worldX / LevelData.Instance.GridCellSize);
        int centerY = Mathf.FloorToInt(worldY / LevelData.Instance.GridCellSize);
        // devide by cell size, round up to grab as many as needed.
        int gridRange = Mathf.CeilToInt(cellRange / LevelData.Instance.GridCellSize);

        // go trough cells from center
        for (int x = centerX - gridRange; x <= centerX + gridRange; x++)
        {
            for (int y = centerY - gridRange; y <= centerY + gridRange; y++)
            {
                Vector2Int key = new(x, y);
                if (EnemyGrid.TryGetValue(key, out List<Entity> celllist))
                {
                    results.AddRange(celllist);
                }
            }
        }
    }

    public List<Entity> GetCellList(Vector2Int cell)
    {
        if (EnemyGrid.TryGetValue(cell, out List<Entity> celllist))
        {
            return celllist;
        }
        return null;
    }



    private void Update()
    {

        for (int i = Enemies.Count - 1; i >= 0; i--)
        {
            Entity enemy = Enemies[i];
            if (enemy != null && enemy.isDead)
            {
                if (EnemyGrid.ContainsKey(enemy.gridPosition))
                    EnemyGrid[enemy.gridPosition].Remove(enemy);

                Enemies.RemoveAt(i);
                Destroy(enemy.gameObject);
                continue;
            }
        }

        if (Enemies.Count == 0)
            return;

        foreach (Entity enemy in Enemies)
        {
            if (enemy == null || enemy.isDead)
                continue;

            // movement

            if (enemy.CanMove)
            {
                // move forwards by speed
                enemy.transform.position = enemy.transform.position + enemy.MovementSpeed * Time.deltaTime * enemy.transform.up;

                // direction from me to target
                Vector3 dir = ( enemy.Target - enemy.transform.position ).normalized;

                // rotate towards angle i want to be
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90), Time.deltaTime * enemy.MovementSpeed * 2);
            }


            // update grid
            int centerX = Mathf.FloorToInt(enemy.transform.position.x / LevelData.Instance.GridCellSize);
            int centerY = Mathf.FloorToInt(enemy.transform.position.y / LevelData.Instance.GridCellSize);

            Vector2Int newKey = new(centerX, centerY);

            // if the position changed, move enemy grid cell.
            if (newKey != enemy.gridPosition)
            {
                // if the grid position doesnt exist, create it.
                MoveEnemy(enemy, newKey);
            }
        }
    }

    public void SpawnEnemy(Entity enemyPrefab, Vector3 position, PathManager Path)
    {
        Entity enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, transform);
        PathFollower pf = enemy.gameObject.AddComponent<PathFollower>();
        pf.Path = Path;

        int centerX = Mathf.FloorToInt(enemy.transform.position.x / LevelData.Instance.GridCellSize);
        int centerY = Mathf.FloorToInt(enemy.transform.position.y / LevelData.Instance.GridCellSize);

        Vector2Int newKey = new(centerX, centerY);

        MoveEnemy(enemy, newKey);

        Enemies.Add(enemy);
    }

    public void MoveEnemy(Entity enemy, Vector2Int newPos)
    {
        if (EnemyGrid.ContainsKey(enemy.gridPosition))
            EnemyGrid[enemy.gridPosition].Remove(enemy);

        if (!EnemyGrid.ContainsKey(newPos))
        {
            EnemyGrid[newPos] = new();
        }
        EnemyGrid[newPos].Add(enemy);
        enemy.gridPosition = newPos;
    }

    public void GetOverlappingEnemies(Vector3 pos, Vector2 size, List<Entity> results)
    {
        List<Entity> nearbyEnemies = new();
        GetEnemiesAt(pos.x, pos.y, size.magnitude, nearbyEnemies);


        results.Clear();
        // rectangle projectile
        Rect a = RectFromCenter(pos, size);

        foreach (Entity enemy in nearbyEnemies)
        {
            // rectangle enemy
            Rect b = RectFromCenter(enemy.transform.position, enemy.transform.localScale);

            if (a.Overlaps(b))
                results.Add(enemy);
        }
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