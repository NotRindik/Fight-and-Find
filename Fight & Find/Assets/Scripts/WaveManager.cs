using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<EnemieCost> enemies = new List<EnemieCost>();
    public int currWave;
    private int waveValue;
    public List<Enemy> enemiesToSpawn = new List<Enemy>();

    public Transform[] spawnLocation;
    public int spawnIndex;

    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    bool isEmpty;

    public List<Enemy> spawnedEnemies = new List<Enemy>();
    void Start()
    {
        GenerateWave();
    }

    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                Enemy enemy =  Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;

                if (spawnIndex + 1 <= spawnLocation.Length - 1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
        foreach (var item in spawnedEnemies)
        {
            if (!item)
                isEmpty = true;
            else
                isEmpty = false;

        }

        if (waveTimer <= 0 && isEmpty)
        {
            currWave++;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<Enemy> generatedEnemies = new List<Enemy>();
        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

}

[System.Serializable]
public class EnemieCost
{
    public Enemy enemyPrefab;
    public int cost;
}

