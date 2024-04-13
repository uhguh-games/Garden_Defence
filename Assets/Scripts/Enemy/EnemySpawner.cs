using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;


public class EnemySpawner : MonoBehaviour
{
    public Transform target;
    public float spawnDelay = 1.5f;
    [SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
    private List<int> enemyAmounts = new List<int>();
    public int enemySpawnAmount = 15;
    public Transform setSpawnPoint;

    [SerializedDictionary("Enemy Amount", "Enemy Pool")]
    public SerializedDictionary<int, ObjectPool> EnemyObjectPools = new SerializedDictionary<int, ObjectPool>();

    void Awake()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            EnemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], enemySpawnAmount));
        }
    }

    public void SetEnemyTypesAndAmounts(List<Enemy> enemyTypes, List<int> amounts)
    {
        enemyPrefabs = enemyTypes;
        enemyAmounts = amounts;
    }

    public IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < enemyAmounts[i]; j++)
            {
                SetSpawnEnemy(i);
                yield return wait;
            }
        }
    }

    void SetSpawnEnemy(int prefabIndex)
    {
        DoSpawnEnemy(prefabIndex);
    }

    void DoSpawnEnemy(int prefabIndex)
    {
        if (prefabIndex < 0 || prefabIndex >= enemyPrefabs.Count)
        {
            Debug.LogWarning("Invalid prefab index.");
            return;
        }

        PoolableObject poolableObject = EnemyObjectPools[prefabIndex].GetObject();

        if (poolableObject != null)
        {
            Enemy enemy = poolableObject.GetComponent<Enemy>();

            Vector3 spawnPosition = setSpawnPoint.position;
            enemy.transform.position = setSpawnPoint.position;

            enemy.Movement.target = target;
            enemy.Agent.enabled = true;
            enemy.Movement.StartChasing();
        }
    }
}