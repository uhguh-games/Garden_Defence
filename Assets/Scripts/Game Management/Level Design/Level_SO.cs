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
    public int wave;
    [TextArea(3,3)] public string description;
    public WaveType waveType;
    public enum WaveType
    {
        normal,
        boss
    }
    public GameObject levelPrefab;

    [Header("Enemies")]

    [SerializedDictionary("Enemy", "Amount")]
    public SerializedDictionary<GameObject, int> EnemiesToSpawn = new SerializedDictionary<GameObject, int>();

}
