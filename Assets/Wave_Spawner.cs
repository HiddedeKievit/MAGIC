using UnityEngine;
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

    private Wave CurrentWave;
    private int CurrentWaveNumber;
    private float nextSpawnTime;

    private bool canSpawn = true;

    private void Update()
    {
        CurrentWave = Waves[CurrentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn && CurrentWaveNumber+1 != Waves.Length)
        {
            CurrentWaveNumber++;
            canSpawn = true;
        }
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = CurrentWave.typeOfEnemies[Random.Range(0, CurrentWave.typeOfEnemies.Length)];
            Transform randomPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            CurrentWave.noOfEnemies--;
            nextSpawnTime = Time.time + CurrentWave.spawnInterval;
            if (CurrentWave.noOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
       
    }
}