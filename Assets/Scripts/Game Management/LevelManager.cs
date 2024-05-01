using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public Level_SO[] levels;
    [SerializeField] private TimeManager timeManager;
    private EnemySpawner enemySpawner;
    [SerializeField] private int currentLevelIndex = 0;
    [SerializeField] float finalDuration = 0f;
    [SerializeField] float timeRate = 8.7f;
    public Level_SO currentLevel = null;
    public Wave_SO currentWave = null;
    GameManager gameManager;
    HealthManager healthManager;
    EconomyManager economyManager;
    HexGrid hexGrid;

    public void Awake() 
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        enemySpawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthManager = GameObject.Find("HealthManager").GetComponent<HealthManager>();
        economyManager = GameObject.Find("EconomyManager").GetComponent<EconomyManager>();
        hexGrid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        timeManager.OnTimeStateChanged += OnTimeStateChangedHandler;
    }

    void Start()
    {
        LoadLevel(currentLevelIndex);
        FindEnemyAmounts();
    }

    void OnTimeStateChangedHandler(TimeState newState)
    {
        LoadLevel(currentLevelIndex);
    }

    void LoadLevel(int levelIndex)
    {
        Level_SO level = levels[levelIndex];
        currentLevel = levels[levelIndex];
    
        // Spawn enemies for the current time of day
        SpawnEnemiesForTimeOfDay(timeManager.currentState, level);
       
    }

    void SpawnEnemiesForTimeOfDay(TimeState timeOfDay, Level_SO level)
    {
        Wave_SO wave = null;

        switch (timeOfDay)
        {
            case TimeState.Morning:
                wave = level.morningWave;
                currentWave = level.morningWave;
                break;
            case TimeState.Day:
                wave = level.dayWave;
                currentWave = level.dayWave;
                break;
            case TimeState.Evening:
                wave = level.eveningWave;
                currentWave = level.eveningWave;
                break;
            case TimeState.Night:
                wave = level.nightWave;
                currentWave = level.nightWave;
                break;
        }

        if (wave != null)
        {
            SpawnEnemiesFromWave(wave);
            print("Spawning enemies for: " + currentLevel + " / " + currentWave);
        }
        else
        {
            Debug.LogWarning("No wave found for the current time of day.");
        }
    }
    
    public void FindEnemyAmounts() // refactor repetitive code later
    {
        foreach (var enemyAmountPair in currentLevel.morningWave.EnemiesToSpawn) 
        {
            float finalDuration = enemyAmountPair.Value + timeRate;
            timeManager.morningDuration = finalDuration;
            timeManager.maxGameDuration += finalDuration;
        }
      
        foreach (var enemyAmountPair in currentLevel.dayWave.EnemiesToSpawn) 
        {
            float finalDuration = enemyAmountPair.Value + timeRate;
            timeManager.dayDuration = finalDuration;
            timeManager.maxGameDuration += finalDuration;
        }
        
        foreach (var enemyAmountPair in currentLevel.eveningWave.EnemiesToSpawn) 
        { 
            float finalDuration = enemyAmountPair.Value + timeRate;
            timeManager.eveningDuration = finalDuration;
            timeManager.maxGameDuration += finalDuration;
        }
      
        foreach (var enemyAmountPair in currentLevel.nightWave.EnemiesToSpawn) 
        {
            float finalDuration = enemyAmountPair.Value + timeRate;
            timeManager.nightDuration = finalDuration;
            timeManager.maxGameDuration += finalDuration;
        }
    }

    void SpawnEnemiesFromWave(Wave_SO wave)
    {
        List<Enemy_AI> enemiesToSpawn = new List<Enemy_AI>();
        List<int> amounts = new List<int>();

        enemiesToSpawn.Clear();
        amounts.Clear();

        foreach (var enemyAmountPair in wave.EnemiesToSpawn) 
        {
            enemiesToSpawn.Add(enemyAmountPair.Key);
            amounts.Add(enemyAmountPair.Value);
        }

        /*
        foreach (var bossAmountPair in wave.BossToSpawn)
        {
            enemiesToSpawn.Add(bossAmountPair.Key);
            amounts.Add(bossAmountPair.Value);
        }
        */

        enemySpawner.SpawnEnemiesFromPools(enemiesToSpawn, amounts);
    }

    public void LoadNextLevel() // the button loads a next level if level was won - replays the same one if lost
    {
        currentLevelIndex++;

        if (currentLevelIndex < levels.Length)
        {
            gameManager.Confirmation();
            healthManager.ResetHealth();
            economyManager.ResetJunk();
            timeManager.ResetTimer();
            //LoadLevel(currentLevelIndex);
            FindEnemyAmounts();
            hexGrid.ClearObjectsInScene();
            timeManager.timeGo = true;
            timeManager.timeIsUp = false;
            // ClearEnemyPools(); // CHEEKY TEST
        }
        else
        {
            Debug.Log("No more levels.");
            healthManager.ResetHealth();
            gameManager.Confirmation();
            // Load next game scene (Level 2 environment)
            // SceneManager.LoadScene(TheNextScene);
            SceneManager.LoadScene("Level_02");
        }
    }

    void ClearEnemyPools()
    {
        enemySpawner.ClearEnemyPools();
    }
}