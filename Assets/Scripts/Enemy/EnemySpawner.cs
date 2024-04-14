using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform target;
    public Transform setSpawnPoint;
    public float spawnDelay;
    [SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
    private List<int> enemyAmounts = new List<int>();

    [Tooltip("Size of each enemy pool")]
    public int enemySpawnAmount = 50; // the amount of each enemy reserved in the POOL
    Dictionary<int, ObjectPool> EnemyObjectPools = new Dictionary<int, ObjectPool>();

    void Awake()
    {
        CreateEnemyPools();
    }

    public void CreateEnemyPools() 
    {
        // EnemyObjectPools.Clear();

        foreach (Enemy enemyPrefab in enemyPrefabs) 
        {
            int prefabIndex = enemyPrefabs.IndexOf(enemyPrefab);
            EnemyObjectPools.Add(prefabIndex, ObjectPool.CreateInstance(enemyPrefab, enemySpawnAmount));
        }
    }

    public void SpawnEnemiesFromPools(List<Enemy> enemiesToSpawn, List<int> amounts)
    {
        StartCoroutine(SpawnEnemies(enemiesToSpawn, amounts));
    }

    private IEnumerator SpawnEnemies(List<Enemy> enemiesToSpawn, List<int> amounts)
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            int prefabIndex = enemyPrefabs.IndexOf(enemiesToSpawn[i]);
            int amount = amounts[i];

            yield return StartCoroutine(SpawnEnemiesFromPool(prefabIndex, amount));
        }
    }
    
    IEnumerator SpawnEnemiesFromPool(int prefabIndex, int amount)
    {
        if (!EnemyObjectPools.ContainsKey(prefabIndex))
        {
            Debug.LogWarning("Enemy pool for prefab index " + prefabIndex + " does not exist.");
            yield break;
        }

        ObjectPool pool = EnemyObjectPools[prefabIndex];
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        for (int i = 0; i < amount; i++)
        {
            PoolableObject poolableObject = pool.GetObject();

            if (poolableObject == null)
            {
                Debug.LogWarning("Object pool is empty.");
                yield break;
            }

            Enemy enemy = poolableObject.GetComponent<Enemy>();

            Vector3 spawnPosition = setSpawnPoint.position;
            enemy.transform.position = spawnPosition;

            enemy.Movement.target = target;
            enemy.Agent.enabled = true;
            enemy.Movement.StartChasing();

            yield return wait;
        }
    }


    public void ClearEnemyPools()
    {
        // Clear the existing enemy pools
        EnemyObjectPools.Clear();
    }
}