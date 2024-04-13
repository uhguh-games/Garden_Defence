using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level_SO[] levels;
    private DayNightState dayNightState; 
    private EnemySpawner enemySpawner;

    // Index to keep track of the current level
    private int currentLevelIndex = 0;

    void Start()
    {
        dayNightState = GameObject.Find("TimeManager").GetComponent<DayNightState>();
        enemySpawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
        
        LoadLevel(currentLevelIndex);
    }

    void LoadLevel(int levelIndex)
    {
        Level_SO level = levels[levelIndex];
        // Spawn enemies for the current time of day
        SpawnEnemiesForTimeOfDay(dayNightState.currentState, level);
    }

    void SpawnEnemiesForTimeOfDay(TimeState timeOfDay, Level_SO level)
    {
        Wave_SO wave = null;

        switch (timeOfDay)
        {
            case TimeState.Morning:
                wave = level.morningWave;
                break;
            case TimeState.Day:
                wave = level.dayWave;
                break;
            case TimeState.Evening:
                wave = level.eveningWave;
                break;
            case TimeState.Night:
                wave = level.nightWave;
                break;
        }

        if (wave != null)
        {
            SpawnEnemiesFromWave(wave);
        }
        else
        {
            Debug.LogWarning("No wave found for the current time of day.");
        }
    }

    void SpawnEnemiesFromWave(Wave_SO wave)
    {
        foreach (var enemyAmountPair in wave.EnemiesToSpawn)
        {
            for (int i = 0; i < enemyAmountPair.Value; i++)
            {
                SpawnEnemy(enemyAmountPair.Key);
            }
        }

        foreach (var bossAmountPair in wave.BossToSpawn)
        {
            for (int i = 0; i < bossAmountPair.Value; i++)
            {
                // SpawnEnemy(bossAmountPair.Key);
            }
        }
    }

    void SpawnEnemy(Enemy enemyPrefab)
    {
        enemySpawner.SpawnEnemy(enemyPrefab);
    }
}
