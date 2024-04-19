using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "New Wave", menuName = "Create new Wave")]
[System.Serializable] 
public class Wave_SO: ScriptableObject
{
    [Header("The Base Script for Waves")]
    [TextArea(2,2)] public string description;

    [Header("Enemies")]

    [SerializedDictionary("Enemy", "Amount")]
    public SerializedDictionary<Enemy_AI, int> EnemiesToSpawn = new SerializedDictionary<Enemy_AI, int>();

    /*[SerializedDictionary("Boss Enemy", "Amount")]
    public SerializedDictionary<Enemy, int> BossToSpawn = new SerializedDictionary<Enemy, int>();
    */

}
