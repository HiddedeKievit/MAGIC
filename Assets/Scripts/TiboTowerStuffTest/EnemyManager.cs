using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<Entity> Enemies;
    private Dictionary<Vector2Int, List<Entity>> EnemyGrid;

    private void Awake()
    {
        if (Instance)
            return;

        EnemyGrid = new();
        Instance = this;
    }


    public void GetEnemiesAt(float worldX, float worldY, float cellRange, List<Entity> results)
    {
        // clear list
        results.Clear();

        // calculate cell position.
        int centerX = Mathf.FloorToInt(worldX / LevelData.Instance.GridCellSize);
        int centerY = Mathf.FloorToInt(worldY / LevelData.Instance.GridCellSize);

        // calculate cell range
        int gridRange = Mathf.CeilToInt(cellRange / LevelData.Instance.GridCellSize);

        // go trough cells
        for (int x = centerX - gridRange; x <= centerX + gridRange; x++)
        {
            for (int y = centerY - gridRange; y <= centerY + gridRange; y++)
            {
                // if cell exists get its entity list
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
        // get entity list if the cell exists
        if (EnemyGrid.TryGetValue(cell, out List<Entity> celllist))
        {
            return celllist;
        }
        return null;
    }



    private void Update()
    {
        // if there are no enemies, dont do anything.
        if (Enemies.Count == 0)
            return;

        // go trough all enemies to check if theres any dead ones
        for (int i = Enemies.Count - 1; i >= 0; i--)
        {
            Entity enemy = Enemies[i];
            if (enemy != null && enemy.isDead)
            {
                // check if the grid cell exists at enemy position then remove from position
                if (EnemyGrid.ContainsKey(enemy.gridPosition))
                    EnemyGrid[enemy.gridPosition].Remove(enemy);

                // remove enemy from general list and destroy
                Enemies.RemoveAt(i);
                Destroy(enemy.gameObject);
                continue;
            }
        }


        foreach (Entity enemy in Enemies)
        {
            // if an enemy reference doesnt exist or if enemy is dead, skip it.
            if (enemy == null || enemy.isDead)
                continue;

            // if enemy can move, move.
            if (enemy.CanMove)
            {
                // move forwards by speed.
                enemy.transform.position = enemy.transform.position + ( enemy.MovementSpeed * Time.deltaTime * enemy.transform.right );

                // direction from me to target
                Vector3 dir = ( enemy.Target - enemy.transform.position ).normalized;

                // rotate towards angle i want to be based on twice the movement speed.
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg), Time.deltaTime * enemy.MovementSpeed * 2);
            }


            // update cell grid
            int centerX = Mathf.FloorToInt(enemy.transform.position.x / LevelData.Instance.GridCellSize);
            int centerY = Mathf.FloorToInt(enemy.transform.position.y / LevelData.Instance.GridCellSize);

            Vector2Int newKey = new(centerX, centerY);

            // if the position changed, move enemy grid cell.
            if (newKey != enemy.gridPosition)
            {
                MoveEnemy(enemy, newKey);
            }
        }
    }

    public void SpawnEnemy(Entity enemyPrefab, Vector3 position, PathManager Path)
    {
        // create enemy from prefab at position using a path

        Entity enemy = Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation, transform);
        PathFollower pf = enemy.gameObject.AddComponent<PathFollower>();
        pf.Path = Path;

        // calculate grid position and move it to the new position
        int centerX = Mathf.FloorToInt(enemy.transform.position.x / LevelData.Instance.GridCellSize);
        int centerY = Mathf.FloorToInt(enemy.transform.position.y / LevelData.Instance.GridCellSize);

        Vector2Int newKey = new(centerX, centerY);

        MoveEnemy(enemy, newKey);

        Enemies.Add(enemy);
    }

    public void MoveEnemy(Entity enemy, Vector2Int newPos)
    {
        // check if the grid position exists, if so remove enemy from it.
        if (EnemyGrid.ContainsKey(enemy.gridPosition))
            EnemyGrid[enemy.gridPosition].Remove(enemy);

        // if new position doesnt exist, make it.
        if (!EnemyGrid.ContainsKey(newPos))
        {
            EnemyGrid[newPos] = new();
        }
        // add enemy to cell and set reference to position
        EnemyGrid[newPos].Add(enemy);
        enemy.gridPosition = newPos;
    }

    public void GetOverlappingEnemies(Vector3 pos, Vector2 hitbox, List<Entity> results)
    {
        // temp list for collecting nearby enemies.
        List<Entity> nearbyEnemies = new();
        GetEnemiesAt(pos.x, pos.y, hitbox.magnitude, nearbyEnemies);


        results.Clear();

        // rectangle for projectile
        Rect a = RectFromCenter(pos, hitbox);

        foreach (Entity enemy in nearbyEnemies)
        {
            // rectangle for enemy
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