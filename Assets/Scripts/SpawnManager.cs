using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab, powerupPrefab;
    float spawnRange = 9;
    public int enemyCount, waveNumber, bossWave = 2, bossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        bossTrigger = bossWave;
    }

    // Update is called once per frame
        void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            SpawnEnemyPrefab(waveNumber);
            SpawnPowerupPrefab();
        }

    }

    void SpawnEnemyPrefab(int enemiesToSpawn)
    {
        if (waveNumber == bossTrigger)
        {
            Instantiate(
                enemyPrefab[2], 
                GenerateSpawnPosition(), 
                enemyPrefab[2].transform.rotation);
            bossTrigger += bossWave;
        }
        else
        {
            for(int i = 0; i < enemiesToSpawn; i++)
            {
                int choseEnemy = Random.Range(0, 2);
                Instantiate(
                    enemyPrefab[choseEnemy], 
                    GenerateSpawnPosition(), 
                    enemyPrefab[choseEnemy].transform.rotation);
            }
            waveNumber++;
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos; 
    }

    public void SpawnPowerupPrefab()
    {
        int chosePowerup = Random.Range(0, 2);
        Instantiate(
            powerupPrefab[chosePowerup], 
            GenerateSpawnPosition(), 
            powerupPrefab[chosePowerup].transform.rotation);
    }
}
