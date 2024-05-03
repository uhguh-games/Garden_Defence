using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    
    public Transform target;
    public Transform setSpawnPoint;
    public float spawnDelay;
    [SerializeField] private List<Enemy_AI> enemyPrefabs = new List<Enemy_AI>();
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

        foreach (Enemy_AI enemyPrefab in enemyPrefabs) 
        {
            int prefabIndex = enemyPrefabs.IndexOf(enemyPrefab);
            EnemyObjectPools.Add(prefabIndex, ObjectPool.CreateInstance(enemyPrefab, enemySpawnAmount));
        }
    }

    public void SpawnEnemiesFromPools(List<Enemy_AI> enemiesToSpawn, List<int> amounts)
    {
        StartCoroutine(SpawnEnemies(enemiesToSpawn, amounts));
    }

    private IEnumerator SpawnEnemies(List<Enemy_AI> enemiesToSpawn, List<int> amounts)
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

            Enemy_AI enemy = poolableObject.GetComponent<Enemy_AI>();

            Vector3 spawnPosition = setSpawnPoint.position;
            enemy.transform.position = spawnPosition;

            enemy.Movement.target = target;
            enemy.Agent.enabled = true;

            enemy.Movement.StartChasing();

            eventManager.EnemySummation();
            eventManager.PlaySFX(1);

            yield return wait;
        }
    }
    
    /*
    public void DisableEnemies() 
    {
       foreach (KeyValuePair<int, ObjectPool> kvp in EnemyObjectPools)
        {
            ObjectPool pool = kvp.Value;
            List<PoolableObject> objectsInPool = pool.GetAllObjects();

            foreach (PoolableObject poolableObject in objectsInPool)
            {
                poolableObject.gameObject.SetActive(false);
            }

            print ("Enemies disabled");
        }
    }
    */

    public void ClearEnemyPools()
    {
        // Clear the existing enemy pools
        EnemyObjectPools.Clear();
    }
}