using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupPrefab;
    public float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    

    void Update() 
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0) 
        {
            int powerupIndex = Random.Range(0,powerupPrefab.Length);
            Instantiate(powerupPrefab[powerupIndex],GenerateSpawnPosition(),powerupPrefab[powerupIndex].transform.rotation);
            SpawnEnemyWave(waveNumber);
            waveNumber ++;
        }
    }

    void Start()
    {
        
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0,enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex],GenerateSpawnPosition(),enemyPrefab[enemyIndex].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-spawnRange,spawnRange);
        float spawnPosZ = Random.Range(-spawnRange,spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX,0.5f,spawnPosZ);
        return randomPos;
    }
}
