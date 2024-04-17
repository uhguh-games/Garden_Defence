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
    public int aphidDrop = 1;
    public int weevilDrop = 2;
    public int beetleDrop = 3;

    [SerializeField] EventManagerSO eventManager;

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
