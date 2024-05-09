using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class EnemyTracker : MonoBehaviour
{
    [SerializedDictionary("Enemy", "Amount")]
    public SerializedDictionary<Enemy_AI, int> EnemiesInScene = new SerializedDictionary<Enemy_AI, int>();

    
    public void UpdateEnemyDictionary(Enemy_AI spawnedEnemy, int amount) 
    {
        EnemiesInScene.Add(spawnedEnemy, amount);

        foreach(KeyValuePair<Enemy_AI, int> kvp in EnemiesInScene)  
        {
            // Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }

    public void AddEnemy(Enemy_AI spawnedEnemy)
    {
        // Check if the enemy already exists in the dictionary
        if (EnemiesInScene.ContainsKey(spawnedEnemy))
        {
            // If it does, increment the count
            EnemiesInScene[spawnedEnemy]++;
        }
        else
        {
            // If not, add it to the dictionary with a count of 1
            EnemiesInScene.Add(spawnedEnemy, 1);
        }
    }

    public void RemoveEnemy(Enemy_AI deadEnemy) 
    {
        // Check if the enemy exists in the dictionary
        if (EnemiesInScene.ContainsKey(deadEnemy))
        {
            // Decrement the count
            EnemiesInScene[deadEnemy]--;

            // If the count reaches 0, remove the enemy from the dictionary
            if (EnemiesInScene[deadEnemy] <= 0)
            {
                EnemiesInScene.Remove(deadEnemy);
            }
        }
    }
}
