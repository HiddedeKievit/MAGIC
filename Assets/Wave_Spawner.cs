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
}