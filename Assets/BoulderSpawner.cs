using UnityEngine;

public class BoulderSpawnerTower : MonoBehaviour
{
    public GameObject boulderPrefab;

    public Transform spawnPoint;

    public float spawnRate = 3f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnRate)
        {
            SpawnBoulder();
            timer = 0f;
        }
    }

    void SpawnBoulder()
    {
        Instantiate(boulderPrefab, spawnPoint.position, Quaternion.identity);
    }
}