using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages everything to do with game economy.
// Keeps track of the players resources
// All prices for things can be modified from the same place
public class EconomyManager : MonoBehaviour
{
    [Header("Players Resources")] // later these could be fetched from a json or something that stores the players stats like funds across different levels/play sessions
    public int playersJunk = 10; // use get set to make this available instead of public?
    // public int playersGold;

    [Space]

    [Header("Costs")]
    // these are not connected to anything yet
    public int fuelCost = 1;
    public int firePitCost = 2;
    public int stoneSlingerCost = 3;

    [Space]

    [Header("Enemy Drops")]
    public int easyDrop = 1;
    public int normalDrop = 2;
    public int hardDrop = 3;
    [SerializeField] EventManagerSO eventManager;

    public delegate void EnemyJunkValueChangeAction();
    public event EnemyJunkValueChangeAction OnEnemyJunkValueChange;

    public int GetEnemyJunkValue(string enemyType) 
    {
        switch (enemyType) 
        {
            case "Easy":
                return easyDrop;
            case "Normal":
                return normalDrop;
            case "Hard":
                return hardDrop;
            default:
                Debug.LogWarning($"Enemy type: {enemyType} not found");
                return 0;
        }
    }

    public void UpdateEnemyJunkValue() 
    {
        OnEnemyJunkValueChange?.Invoke();
    }

    void OnEnable() 
    {
        eventManager.junkCollect += CollectJunk;
    }

    void OnDisable() 
    {
        eventManager.junkCollect -= CollectJunk;
    }

    public void SpendJunk(int cost) 
    {
        playersJunk -= cost;
    }
    public void CollectJunk(int amount) 
    {
        print (amount);
        playersJunk += amount;
    }
}
