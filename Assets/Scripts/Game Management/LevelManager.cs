using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelManager : MonoBehaviour
{
    public Level_SO[] levels;
    [SerializeField] private DayNightState dayNightState;
    private EnemySpawner enemySpawner;
    [SerializeField] private int currentLevelIndex = 0;

    void Start()
    {
        dayNightState = GameObject.Find("TimeManager").GetComponent<DayNightState>();
        enemySpawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
        
        // Subscribe to the OnTimeStateChanged event
        dayNightState.OnTimeStateChanged += OnTimeStateChangedHandler;

        // Load the initial level
        LoadLevel(currentLevelIndex);
    }


    void LoadLevel(int levelIndex)
    {
        Level_SO level = levels[levelIndex];
        
        // Spawn enemies for the current time of day
        SpawnEnemiesForTimeOfDay(dayNightState.currentState, level);
    }
    void OnTimeStateChangedHandler(TimeState newState)
    {
        LoadLevel(currentLevelIndex);
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
        List<Enemy> enemiesToSpawn = new List<Enemy>();
        List<int> amounts = new List<int>();

        foreach (var enemyAmountPair in wave.EnemiesToSpawn)
        {
            enemiesToSpawn.Add(enemyAmountPair.Key);
            amounts.Add(enemyAmountPair.Value);
        }

        foreach (var bossAmountPair in wave.BossToSpawn)
        {
            enemiesToSpawn.Add(bossAmountPair.Key);
            amounts.Add(bossAmountPair.Value);
        }

        enemySpawner.SetEnemyTypesAndAmounts(enemiesToSpawn, amounts);
        StartCoroutine(enemySpawner.SpawnEnemies());
    }
}