using UnityEngine.AI;
using UnityEngine;

// This script holds the Base Stats for each enemy
[CreateAssetMenu(fileName = "Enemy", menuName = "Create new Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Stats")]
    public int Health;
    public int JunkDropAmount; // how can I get this to be set by the EconomyManager?
    // other stats drop types, drop rates, behavior, how much damage they deal etc.

    [Header("NavMeshAgent Configuration")]
    public float AIUpdateInterval = 0.1f;
    public float Acceleration = 8f;
    public float AngularSpeed = 120f;
    public int AreaMask = -1; // -1 means everything
    public int AvoidancePriority = 50;
    public float BaseOffset = 0;
    public float Height = 2f;
    // public UnityEngine.AI.ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float Radius = 0.5f;
    public float Speed = 3f; // nav mesh agent speed
    public float StoppingDistance = 0.5f;
 
}
