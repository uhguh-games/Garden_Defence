using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform target;
    public int enemySpawnAmount = 5; // how many enemies we want to spawn
    public float spawnDelay = 1f;
    public List<Enemy> enemyPrefabs = new List<Enemy>();
    public Transform setSpawnPoint;
    private Dictionary<int, ObjectPool> EnemyObjectPools = new Dictionary<int, ObjectPool>();

    void Awake()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            EnemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], enemySpawnAmount));
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        int enemySpawnCounter = 0; // keeps track of the amount of enemies that have spawned

        while (enemySpawnCounter < enemySpawnAmount)
        {
            SetSpawnEnemy();
            
            enemySpawnCounter++;

            yield return wait;
        }
    }

    void SetSpawnEnemy()
    {
        DoSpawnEnemy(-1);
    }

    void DoSpawnEnemy(int spawnIndex)
    {
        PoolableObject poolableObject;

        if (spawnIndex >= 0)
        {
            poolableObject = EnemyObjectPools[spawnIndex].GetObject();
        }
        else // Spawn at set spawn point
        {
            Vector3 spawnPosition = setSpawnPoint.position;
            UnityEngine.AI.NavMeshHit hit;

            if (UnityEngine.AI.NavMesh.SamplePosition(spawnPosition, out hit, 50f, UnityEngine.AI.NavMesh.AllAreas))
            {
                spawnPosition = hit.position;
            }
            else
            {
                Debug.LogWarning("Failed to find valid position on NavMesh for spawn point.");
            }

            poolableObject = EnemyObjectPools[0].GetObject(); // Assuming the first enemy prefab is the one to spawn
            poolableObject.transform.position = spawnPosition;
        }

        if (poolableObject != null)
        {
            Enemy enemy = poolableObject.GetComponent<Enemy>();

            enemy.transform.position = setSpawnPoint.position;

            enemy.Movement.target = target;
            enemy.Agent.enabled = true;
            enemy.Movement.StartChasing();
        }
    }
}
