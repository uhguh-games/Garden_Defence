using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "New Level", menuName = "Create new Level")]
[System.Serializable] 
public class Level_SO: ScriptableObject
{
    [Header("The Base Script for Levels")]

    public int levelNumber;
    public Wave_SO morningWave;
    public Wave_SO dayWave;
    public Wave_SO eveningWave;
    public Wave_SO nightWave;
    // public GameObject levelPrefab; // later maybe
    


}
