using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class Wave_Spawner : MonoBehaviour
{
    public Wave[] Waves;
    public Transform[] SpawnPoints;

    public float timeBetweenWaves = 10f; 

    public PathManager Path;

    public Text waveCounterText;  

    private int currentWaveNumber = 0;
    private Wave currentWave;

    private float nextSpawnTime;
    private float waveCountdown;

    private int enemiesLeftToSpawn;

    private bool isSpawning = false;
    private bool isWaitingForNextWave = false;

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        // If currently spawning enemies
        if (isSpawning)
        {
            SpawnWave();
        }

        // If waiting for next wave timer
        if (isWaitingForNextWave)
        {
            waveCountdown -= Time.deltaTime;

            if (waveCountdown <= 0f)
            {
                currentWaveNumber++;

                if (currentWaveNumber < Waves.Length)
                {
                    StartWave();
                }
                else
                {
                    Debug.Log("All waves completed!");
                    isWaitingForNextWave = false;
                }
            }
        }
    }

    void StartWave()
    {
        currentWave = Waves[currentWaveNumber];
        enemiesLeftToSpawn = currentWave.noOfEnemies;

        isSpawning = true;
        isWaitingForNextWave = false;

        nextSpawnTime = Time.time;
        UpdateWaveUI();
    }

    void SpawnWave()
    {
        if (Time.time >= nextSpawnTime && enemiesLeftToSpawn > 0)
        {
            GameObject randomEnemy =
                currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
        
           

            Transform randomPoint =
                SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            GameObject newenemy = Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            PathFollower pf = newenemy.AddComponent<PathFollower>();
            pf.Path = Path;

            enemiesLeftToSpawn--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
        }

        
        if (enemiesLeftToSpawn == 0 && isSpawning)
        {
            isSpawning = false;
            isWaitingForNextWave = true;
            waveCountdown = timeBetweenWaves;
        }
    }

    void UpdateWaveUI()
{
    waveCounterText.text = "Wave: " + (currentWaveNumber + 1) + " / " + Waves.Length;
}
}